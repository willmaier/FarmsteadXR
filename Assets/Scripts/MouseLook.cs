using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100.0f;
    float yaw = 0;
    float pitch = 0;

    Transform playerBody;


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerBody = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //yaw
        // yaw += moveX;
        playerBody.Rotate(Vector3.up * moveX);


        //pitch
        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, -75f, 75f);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0.0f);

    }
}
