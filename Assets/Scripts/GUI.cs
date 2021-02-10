using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour, IPlayerHP
{
    [SerializeField] private Image hp = null;
    [SerializeField] private Text tasks = null;
    [SerializeField] private Image panel = null;
    // Start is called before the first frame update



    private void Awake()
    {
        change_task("Активировать рычаг");
        panel.gameObject.SetActive(false);
    }

    public void show_panel(bool show)
    {
        if (show)
        panel.gameObject.SetActive(true);
        else
        panel.gameObject.SetActive(false);
    }

    public void change_hp(float health)
    {
        hp.fillAmount = health/100;
    }

    public void change_task(string task)
    {
        tasks.text = task;
    }
}
