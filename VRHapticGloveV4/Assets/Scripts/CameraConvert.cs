using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConvert : MonoBehaviour
{
    public GameObject rigCamera, bridViewCamera;
    [Range(1,5)]
    public float movementSensitivity = 2f;
    [Range(0.1f, 3)]
    public float mouseSensitivity = 1;
    float rotationX, rotationY;
    void Update()
    {        
        if (Input.GetKeyDown("c"))
        {
            if (rigCamera.activeSelf)
            {
                rigCamera.SetActive(false);
                bridViewCamera.SetActive(true);
            }
            else
            {
                rigCamera.SetActive(true);
                bridViewCamera.SetActive(false);
            }
            
        }

        if (bridViewCamera.activeSelf)
        {
            //camera rotation
            rotationX += Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
            bridViewCamera.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);

            //camera position
            if (Input.GetKey("w"))
            {
                bridViewCamera.transform.Translate(Vector3.forward * movementSensitivity / 100);
            }
            if (Input.GetKey("s"))
            {
                bridViewCamera.transform.Translate(Vector3.back * movementSensitivity / 100);
            }
            if (Input.GetKey("a"))
            {
                bridViewCamera.transform.Translate(Vector3.left * movementSensitivity / 100);
            }
            if (Input.GetKey("d"))
            {
                bridViewCamera.transform.Translate(Vector3.right * movementSensitivity / 100);
            }
        }
    }
}
