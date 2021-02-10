using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour, IStairsDown
{
    private bool startdown = false;
    private bool down = false;
    public Vector3 stair_down;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartDown()
    {
        startdown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startdown && !down)
        {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, stair_down, 0.01f));

            if (transform.rotation.eulerAngles == stair_down)
            {
                startdown = false;
                down = true;
            }
        }
    }
}
