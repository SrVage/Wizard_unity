using UnityEngine;

public class Key : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Rotate(0, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject door = GameObject.Find("door");
            door.GetComponent<IGetKey>().GetKey();
            Destroy(gameObject);
            GameObject gui = GameObject.Find("Canvas");
            gui.GetComponent<IPlayerHP>().change_task("Открыть комнату босса");
        }
    }
}
