using UnityEngine;

public class CameraMove : MonoBehaviour
/*{
    //private float osx;
    public float sensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    //Vector3 cameraPosition = playerBody.transform.position;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //osx = Input.GetAxis("Mouse X");
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.transform.position += new Vector3(xRotation*sensitivity, 0, 0);
        //playerBody.Rotate(Vector3.up * mouseX);
        //if(Input.GetMouseButtonDown(0))
        //{
            
        //}
    }
}*/
{
    private Vector3 pozycjaMyszy;
    private Vector3 pozycjaObiektu;
    public float sensitive = 100f;
    public float maxLeft = -10428.28f;
    public float maxRight = 11506.25f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pozycjaMyszy = Input.mousePosition;
            pozycjaObiektu = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            float roznicaPozycjiMyszy = Input.mousePosition.x - pozycjaMyszy.x;
            Vector3 nowaPozycjaObiektu = new Vector3(pozycjaObiektu.x - roznicaPozycjiMyszy*sensitive / 100f, pozycjaObiektu.y, pozycjaObiektu.z);
            if (nowaPozycjaObiektu.x > maxLeft && nowaPozycjaObiektu.x < maxRight)
            {
                transform.position = nowaPozycjaObiektu;
            }
        }
    }
}