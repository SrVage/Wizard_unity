using UnityEngine;

public class TurelBall : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    private Rigidbody rb = null;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GameObject player = GameObject.Find("Player");
      GameObject.Destroy(gameObject, 5);
    }
    void Update()
    {
     transform.Translate(Vector3.forward*Time.deltaTime*speed);  
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        other.GetComponent<IPlayerDamage>().PlayerDamage(10);
        Destroy(gameObject);
        }

        
    }
}
