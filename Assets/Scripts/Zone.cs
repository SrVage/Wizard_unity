using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private GameObject button = null;
    [SerializeField] private GameObject pos = null;
    public bool hide = true;
    private bool cr = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        hide = false;
       
    }

    private void OnTriggerExit(Collider other)
    {
        hide = true;
        cr = false;
    }

    private void Update()
    {
        if (!hide && !cr)
        {
            Instantiate(button, pos.transform.position, Quaternion.identity);
            cr = true;
        }
    }
}
