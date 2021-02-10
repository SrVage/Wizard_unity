using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Image _start = null;
    [SerializeField] private Image _exit = null;
    [SerializeField] private Image _preferences = null;
    [SerializeField] private Image _edit = null;
    [SerializeField] private Image _back = null;
    // Start is called before the first frame update

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void open_Preferences()
    {
        _edit.gameObject.SetActive(true);
    }

    public void close_Preferences()
    {
        _edit.gameObject.SetActive(false);
    }

    public void start_game()
    {
        SceneManager.LoadScene("1");
    }

    public void quick_game()
    {
        Application.Quit();
    }

}
