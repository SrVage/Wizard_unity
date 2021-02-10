using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy2 : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int hp = 10; //здоровье врага
    [SerializeField] private GameObject player_target = null;
    private bool _player_vis = false; //виден ли игрок
    private Rigidbody rb;
    [SerializeField] private GameObject _raycast = null;
    private Animator animator = null;
    private bool death = false;
    [SerializeField] private ParticleSystem ps = null;
    private AudioSource sound = null;
    [SerializeField] AudioClip attack = null;
    [SerializeField] AudioClip fall = null;
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
                //animator.SetTrigger("attack");
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
        rb.isKinematic = true;
        sound.volume = 0.5f;
        sound.PlayOneShot(fall);
        death = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        animator.SetTrigger("death");
        Destroy(this);
     //Destroy(gameObject);
    }

    public void Force(GameObject bomb)
    {
        if (death) return;
        rb.AddForce((transform.position-bomb.transform.position)*500, ForceMode.Impulse);
        StartCoroutine("Boom");
    }
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(0.05f); //даем время для отрыва от земли
        while (transform.position.y>2) yield return null;
            TakeDamage(5);
            StopCoroutine("Boom");
    }

    private void Attack()
    {
        var bul = Instantiate(bulet, _raycast.transform.position, _raycast.transform.rotation);
       // player_target.GetComponent<IPlayerDamage>().PlayerDamage(7);
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
    }
}