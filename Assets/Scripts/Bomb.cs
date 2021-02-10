using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            other.gameObject.GetComponent<ITakeDamage>().Force(this.gameObject);
            GameObject player = GameObject.Find("Player");
            player.GetComponent<PlayerMove>().trurBomb();
        }
        Destroy(gameObject, 0.4f);
    }
}
