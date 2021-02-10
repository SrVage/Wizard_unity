using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy1 : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int hp = 10; //здоровье врага
    public Transform[] target = new Transform[4]; //точки навигации
    [SerializeField] private GameObject player_target = null;
    NavMeshAgent agent = new NavMeshAgent();
    private int num = 0;
    private bool _player_vis = false; //виден ли игрок
    private Rigidbody rb;
    [SerializeField] private GameObject _raycast = null;
    private Animator animator = null;
    private bool death = false;
    [SerializeField] private ParticleSystem ps = null;
    private AudioSource sound = null;
    [SerializeField] AudioClip step = null;
    [SerializeField] AudioClip fall = null;


    private void FixedUpdate()
    {
        if (death) return;
        RaycastHit hit;
        if (Physics.Raycast(_raycast.transform.position, _raycast.transform.position-transform.position, out hit))
        {
          if (hit.collider.gameObject.CompareTag("Player"))
            {
                _player_vis = true;
            }
            StartCoroutine("MissingPlayer");
        }
    }

    IEnumerator MissingPlayer ()
    {
        yield return new WaitForSeconds (4f);
        _player_vis = false;
        StopCoroutine("MissingPlayer");
    }

    private void Step()
    {
        sound.volume = 0.1f;
        sound.PlayOneShot(step);
    }

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target[num].position);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
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
        sound.volume = 0.5f;
        sound.PlayOneShot(fall);
        death = true;
        agent.enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        animator.SetTrigger("death");
        Destroy(this);
     //Destroy(gameObject);
    }

    public void Force(GameObject bomb)
    {
        if (death) return;
        rb.isKinematic = false;
        agent.enabled = false; //отключаем Nav Mesh Agent для добавления импульса силы
        rb.AddForce((transform.position-bomb.transform.position)*500, ForceMode.Impulse);
        StartCoroutine("Boom");
    }
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(0.05f); //даем время для отрыва от земли
        while (transform.position.y>2) yield return null;
            rb.isKinematic = true;
            agent.enabled = true;
            TakeDamage(5);
            StopCoroutine("Boom");
    }

    private void OnTriggerStay(Collider other)
    {
        if (death) return;
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attack", true);
            agent.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (death) return;
        animator.SetBool("attack", false);
            agent.enabled = true;
    }

    private void Attack()
    {
        player_target.GetComponent<IPlayerDamage>().PlayerDamage(15);
    }

    void Update()
    {
        if (death) return;
        if (agent.enabled)
        {
            if (!_player_vis) //если не видим игрока, то идем по маршруту
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                animator.SetBool("run", false);
                agent.speed = 2.5f;
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    num++;
                    if (num > 3) num = 0;
                    agent.SetDestination(target[num].position);
                }
            }
            else
            {
                animator.SetBool("run", true);
                gameObject.GetComponent<BoxCollider>().enabled = true;
                agent.speed = 5f;
                agent.SetDestination(player_target.transform.position);
            }
        }
    }
}