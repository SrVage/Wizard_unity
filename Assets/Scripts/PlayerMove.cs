using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour, IPlayerDamage
{
    private Vector3 direction = Vector3.zero;
    private bool bomb=false;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float angspeed = 10f;
    [SerializeField] private GameObject fireball = null;
    [SerializeField] private GameObject fireplace = null;
    [SerializeField] private GameObject boom = null;
    private Vector3 angle = Vector3.zero;
    [SerializeField] private float player_hp=100f;
    [SerializeField] private GameObject grenade = null;
    private Rigidbody rb = new Rigidbody();
    private Animator animator = null;
    private bool _isAlive = true;
    [SerializeField] private GameObject grenade_pos = null;
    [SerializeField] private GameObject gui = null;
    private AudioSource sound = null;
    [SerializeField] AudioClip fire = null;
    [SerializeField] AudioClip damage = null;
    [SerializeField] AudioClip death = null;
    [SerializeField] AudioClip step = null;
    private bool onGround = false;
    [SerializeField] private ParticleSystem ps = null;
    private GameObject soundmanager = null;
    [SerializeField] GameObject _pause = null;


    private void Awake()
    {
        soundmanager = GameObject.Find("SoundManager");
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!_isAlive) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) animator.SetTrigger("attack");
        if (Input.GetKeyDown(KeyCode.Mouse1) && !bomb)
        {
            bomb = true;
            Instantiate(boom, fireplace.transform.position, fireplace.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.G)) animator.SetTrigger("grenade");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pause.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        { 
            Time.timeScale = 1;
            _pause.gameObject.SetActive(false);
        }
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        angle.y = Input.GetAxis("Mouse X");
        if (Input.GetKeyDown(KeyCode.Space) && onGround) rb.AddForce(Vector3.up*500, ForceMode.Impulse);
    }

    private void Grenade()
    {
        Instantiate(grenade, grenade_pos.transform.position, fireplace.transform.rotation);
    }

    private void FixedUpdate()
    {
        if (!_isAlive) return;
        var s = direction * speed * Time.fixedDeltaTime;
        if (s != Vector3.zero) animator.SetBool("move", true);
        else animator.SetBool("move", false);
        var a = angle * angspeed * Time.fixedDeltaTime;
        transform.Translate(s);
        transform.Rotate(a);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                onGround = true;
            }
          
        }
      else onGround = false;
    }

    public void trurBomb()
    {
        bomb = false;
    }

    private void Fire()
    {
        sound.volume = 1.0f;
        sound.PlayOneShot(fire);
        Instantiate(fireball, fireplace.transform.position, fireplace.transform.rotation);
    }

    private void Step()
    {
        sound.volume = 0.3f;
        sound.PlayOneShot(step);
    }

    public void PlayerDamage(float player_damage)
    {
        if (!_isAlive) return;
        sound.volume = 1.0f;
        sound.PlayOneShot(damage);
        ps.Play();
        animator.SetTrigger("damage");
        player_hp -= player_damage;
        if (player_hp <= 0) EndGame();
        gui.GetComponent<IPlayerHP>().change_hp(player_hp);
        soundmanager.GetComponent<SoundManager>().ChangeTemp(player_hp);

    }

    private void EndGame()
    {
        if (_isAlive)
        {
            _isAlive = false;
            animator.SetTrigger("death");
            sound.volume = 1.0f;
            sound.PlayOneShot(death);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            Destroy(GameObject.Find("SoundManager"));
        }
        StartCoroutine("exit");
       // Destroy(gameObject);
       // Application.Quit();
    }

    IEnumerator exit()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("Menu");
    }
}
