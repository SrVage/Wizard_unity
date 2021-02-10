using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject slider = null;
    private AudioSource sound = null;
    // Start is called before the first frame update
    private void Awake()
    {
            sound = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);     
    }

    public void change_Volume()
    {
        sound.volume = slider.GetComponent<Slider>().value;
    }

    public void ChangeTemp(float health)
    {
        sound.pitch = 1.25f - health / 400;
    }
}
