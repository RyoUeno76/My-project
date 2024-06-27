using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLook : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float mousesensitivity;
    public Transform playerBody;
    float xRotation = 0f;
    bool Rotate = true;
    public GameObject Controller;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && Rotate)
        {
            Rotate = false;
        }
        if (Input.GetKeyDown(KeyCode.F) && Rotate == false)
        {
            Rotate = true;
        }

        if (Rotate)
        {
            UpdateLook();
        }
        if (Rotate == false)
        {
            UpdateMovement();
        }
        
    }

    void UpdateLook()
    {
        float mouseX = Input.GetAxis("Horizontal") * mousesensitivity * Time.deltaTime;
        float mousey = Input.GetAxis("Vertical") * mousesensitivity * Time.deltaTime;

        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void UpdateMovement()
    {
        
    }
}
