using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    // Start is called before the first frame update
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, player.transform.position, 7*Time.smoothDeltaTime);
        //transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, player.transform.rotation.eulerAngles, 20 * Time.smoothDeltaTime));
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 9 * Time.fixedDeltaTime);
        transform.rotation = player.transform.rotation;
       // transform.rotation = Quaternion.Euler(Vector3.Slerp(transform.rotation.eulerAngles, player.transform.rotation.eulerAngles, 8 * Time.fixedDeltaTime));

    }
}