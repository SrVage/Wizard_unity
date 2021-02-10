using UnityEngine;

public class Door : MonoBehaviour, IGetKey
{
    public Vector3 target;
    [SerializeField] private bool startopen = false;
    [SerializeField] private bool _open = false;
    [SerializeField] private bool key = false;
    [SerializeField] GameObject boss = null;
    private Animator animator = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other)
    {
        GameObject gui = GameObject.Find("Canvas");
        gui.GetComponent<IPlayerHP>().show_panel(true);
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && key)
        {
            startopen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject gui = GameObject.Find("Canvas");
        gui.GetComponent<IPlayerHP>().show_panel(false);
    }
    public void GetKey()
    {
        key = true;
    }
    void Update()
    {
        if (startopen && !_open)
        {
            animator.SetBool("open", true);
            //transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, target, 0.01f));
                    
                startopen = false;
                _open = true;
            GameObject gui = GameObject.Find("Canvas");
            gui.GetComponent<IPlayerHP>().change_task("Убить босса");
            boss.gameObject.SetActive(true);
        }
    }
}
