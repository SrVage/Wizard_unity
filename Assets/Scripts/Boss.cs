using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int hp = 10; //здоровье врага
    [SerializeField] private GameObject player_target = null;
    private bool _player_vis = false; //виден ли игрок
    private Rigidbody rb;
    [SerializeField] private GameObject _raycast = null;
    private Animator animator = null;
    private bool death = false;
    [SerializeField] private ParticleSystem ps = null;
    [SerializeField] private ParticleSystem end = null;
    private AudioSource sound = null;
    [SerializeField] AudioClip attack = null;
    [SerializeField] AudioClip death_cry = null;
    [SerializeField] GameObject bulet = null;
    private float time = 3.0f;


    private void FixedUpdate()
    {
        if (death)            return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player_target.transform.position - transform.position, out hit, 30f))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                transform.LookAt(player_target.transform);
                _player_vis = true;
            }
            else _player_vis = false;
        }
        else _player_vis = false;
    }

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage (int damage)
    {
        ps.Play();
        if (death) return;
        animator.SetTrigger("damage");
        hp -= damage;
        if (hp <= 0) Death();
    }

    private void Death()
    {
        sound.volume = 1.0f;
        sound.PlayOneShot(death_cry);
        rb.isKinematic = false;
        death = true;
        end.Play();
        animator.SetTrigger("death");
        Destroy(GameObject.Find("SoundManager"));
        StartCoroutine("WinScene");
    }

   IEnumerator WinScene()
    {
        yield return new WaitForSeconds(7.0f);
        SceneManager.LoadScene("Menu");
    }

    public void Force(GameObject bomb)
    {
        if (death) return;
        TakeDamage(5);
    }


    private void Fire()
    {
        sound.PlayOneShot(attack);
        var bul = Instantiate(bulet, _raycast.transform.position, _raycast.transform.rotation);
    }

    void Update()
    {
        if (death) return;
        time -= Time.deltaTime;
        if (_player_vis && time <= 0)
        {
            animator.SetTrigger("attack");
            time = 2.0f;
        }
        if (!_player_vis)
        {
            transform.Translate((player_target.transform.position - transform.position) * Time.deltaTime * 0.2f, Space.World);
            transform.position.Set(transform.position.x, 9.22f, transform.position.z);
        }
    }
}