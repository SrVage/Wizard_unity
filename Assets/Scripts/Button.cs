using UnityEngine;

public class Button : MonoBehaviour
{
    public Vector3 angle;
    private AudioSource sound = null;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }
    private void OnTriggerStay(Collider other)
    {
GameObject gui = GameObject.Find("Canvas");
        gui.GetComponent<IPlayerHP>().show_panel(true);
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            sound.Play();
            GameObject stair = GameObject.Find("stairs");
            stair.GetComponent<IStairsDown>().StartDown();
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, angle, 0.01f));
            
            gui.GetComponent<IPlayerHP>().change_task("Найти ключ на втором этаже");
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
            GameObject gui = GameObject.Find("Canvas");
        gui.GetComponent<IPlayerHP>().show_panel(false);
    }

    private void Update()
    {
        GameObject gb = GameObject.Find("Zone");
        if (gb.GetComponent<Zone>().hide) Destroy(gameObject);
    }
}
