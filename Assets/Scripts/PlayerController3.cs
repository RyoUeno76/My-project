using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3: MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float VertmouseSensitivity = 10;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 1f)] float moveSmoothTime = 1f;
    [SerializeField] [Range(0.0f, 1f)] float mouseSmoothTime = 1f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float VertcameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    bool MoveOrRotate;
    float inputZ, inputX;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        MoveOrRotate = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && MoveOrRotate == true)
        {
            MoveOrRotate = false;
        }
        else if (Input.GetKeyDown(KeyCode.F) && MoveOrRotate == false)
        {
            MoveOrRotate = true;
        }
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        if (MoveOrRotate == true)
        {
            UpdateMouseLook();
        } else if(MoveOrRotate == false) {
            UpdateMovement();
        }

    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(inputX, inputZ);

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        VertcameraPitch -= currentMouseDelta.y * VertmouseSensitivity;
        VertcameraPitch = Mathf.Clamp(VertcameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * VertcameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(inputX, inputZ);
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

    }

}