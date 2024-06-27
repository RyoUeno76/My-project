using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController2: MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float health = 100;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float VertmouseSensitivity = 10;
    [SerializeField] float walkSpeed = 15.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 1f)] float moveSmoothTime = 1f;
    [SerializeField] [Range(0.0f, 1f)] float mouseSmoothTime = 1f;
    public GameObject assault1;
    public bool equiped;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float VertcameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    public float pickUpRange = 5;
    public float moveForce = 250;
    public Transform holdParent;
    private GameObject heldObj;


    bool MoveOrRotate;
    float inputZ, inputX;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
    enum MovementMode
    {
        Strafe
    }
    [SerializeField]
    private MovementMode _movementMode = MovementMode.Strafe;
    [SerializeField]
    private float _walkSpeed = 3f;
    [SerializeField]
    private float _runningSpeed = 6f;
    [SerializeField]
    private float _gravity = 9.81f;
    [SerializeField]
    private float _gravityPlatformer = -12f;
    [SerializeField]
    private float _jumpSpeed = 3.5f;
    [SerializeField]
    private float _doubleJumpMultiplier = 0.5f;
    [SerializeField]
    private GameObject _cameraRig;
    public float jumpHeight = 1;

    private CharacterController _controller;

    private float _directionY;
    private float _currentSpeed;

    private bool _canDoubleJump = false;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    public  bool Left = false;
    public bool Right = false;
    public HealthBar bar;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        MoveOrRotate = true;
        equiped = true;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    PickUpObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        if (heldObj != null)
        {
            MoveObject();
        }
        if (LeftButton.Left == true)
        {
            UpdateLeft();
        } 
        else if(RightButton.Right == true)
        {
            UpdateRight();
        }
        
        if (MoveOrRotate == true)
        {
            UpdateMouseLook();
        } else if(MoveOrRotate == false) {
            UpdateMovement();
        }
        if (Input.GetKeyDown(KeyCode.E) && equiped == true)
        {
            hide();
            equiped = false;
            Debug.Log("fuck");
        }
        else if (Input.GetKeyDown(KeyCode.E) && equiped == false)
        {
            show();
            equiped = true;
            Debug.Log("shit");
        }
    }
    public void hide()
    {
        assault1.SetActive(false);
    }
    public void show()
    {
        assault1.SetActive(true);
    }
    void UpdateLeft()
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
    }
    void UpdateRight()
    {
        if (Input.GetKeyDown(KeyCode.J) && MoveOrRotate == true)
        {
            MoveOrRotate = false;
        }
        else if (Input.GetKeyDown(KeyCode.J) && MoveOrRotate == false)
        {
            MoveOrRotate = true;
        }
        inputX = Input.GetAxis("Horizontal1");
        inputZ = Input.GetAxis("Vertical1");
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
        if(LeftButton.Left == true)
        {
            UpdateMovementLeft();
        }
        else if(RightButton.Right == true)
        {
            UpdateMovementRight();
        }

    }
    void UpdateMovementLeft()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (_controller.isGrounded)
        {
            _canDoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                _directionY = _jumpSpeed;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && _canDoubleJump)
            {
                _directionY = _jumpSpeed * _doubleJumpMultiplier;
                _canDoubleJump = false;
            }
        }

        _directionY -= _gravity * Time.deltaTime;

        moveDirection = transform.TransformDirection(moveDirection);

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = (running) ? _runningSpeed : _walkSpeed;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        moveDirection.y = _directionY;

        _controller.Move(_currentSpeed * Time.deltaTime * moveDirection);
    }
    void UpdateMovementRight()
    {
        float horizontalInput = Input.GetAxis("Horizontal1");
        float verticalInput = Input.GetAxis("Vertical1");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        if (_controller.isGrounded)
        {
            _canDoubleJump = true;

            if (Input.GetButtonDown("Jump1"))
            {
                _directionY = _jumpSpeed;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump1") && _canDoubleJump)
            {
                _directionY = _jumpSpeed * _doubleJumpMultiplier;
                _canDoubleJump = false;
            }
        }

        _directionY -= _gravity * Time.deltaTime;

        moveDirection = transform.TransformDirection(moveDirection);

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = (running) ? _runningSpeed : _walkSpeed;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        moveDirection.y = _directionY;

        _controller.Move(_currentSpeed * Time.deltaTime * moveDirection);
    }
    public void take_damage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("L");
        if(collision.gameObject.tag == "bullet")
        {
            Debug.Log("W");
            take_damage(20);
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }
    void PickUpObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 10;

            objRig.transform.parent = holdParent;
            heldObj = pickObj;
        }
    }
    void DropObject()
    {
        Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.drag = 1;

        heldObj.transform.parent = null;
        heldObj = null;
    }
}