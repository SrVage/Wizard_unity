using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float speed = 30f;

    private void Awake()
    {
      GameObject.Destroy(gameObject, 5);
    }

    void Update()
    {
      transform.Translate(Vector3.forward*Time.deltaTime*speed);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            other.GetComponent<ITakeDamage>().TakeDamage(5);
        }
        Destroy(gameObject);
    }
}
