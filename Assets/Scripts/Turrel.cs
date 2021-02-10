using UnityEngine;

public class Turrel : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject bulet = null;
    [SerializeField] GameObject target = null;
    private bool fire = false;
    private float timer = 0f;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.LookAt(player.transform);
            fire = true;
        }
    }
    private void FixedUpdate()
    {
        if (fire && timer<=0)
        {
        var bul = Instantiate(bulet, target.transform.position, target.transform.rotation);
            timer = 1.0f; //сбрасываем таймер перезарядки
            fire = false;
        }
    }
    void Update()
    {
        if (timer >= 0)            timer -= Time.deltaTime; //счет таймера
    }
}
