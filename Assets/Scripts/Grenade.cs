using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody rb = new Rigidbody();
    [SerializeField] ParticleSystem ps = null;
    private GameObject player = null;
    private float time = 3.0f;
    private AudioSource boom = null;
    private bool play = false;
    // Start is called before the first frame update
    private void Awake()
    {
        boom = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        rb.AddForce((transform.position-player.transform.position)*8, ForceMode.Impulse);
        ps.Play();
        Destroy(gameObject, 4f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")&&time<=0) other.gameObject.GetComponent<ITakeDamage>().Force(this.gameObject);
    }

    private void playboom()
    {
        play = true;
        boom.Play();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0 && play == false) playboom();
    }
}
