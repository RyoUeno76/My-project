using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class player3 : MonoBehaviour
{
    enum MovementMode
    {
        Platformer,
        Strafe
    }
    private bool MoveOrRotate;
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
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float VertmouseSensitivity = 10;
    [SerializeField] [Range(0.0f, 1f)] float moveSmoothTime = 1f;
    [SerializeField] [Range(0.0f, 1f)] float mouseSmoothTime = 1f;
    float cameraPitch = 0.0f;
    float VertcameraPitch = 0.0f;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
    public float jumpHeight = 1;

    private CharacterController _controller;

    private float _directionY;
    private float _currentSpeed;

    private bool _canDoubleJump = false;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float inputZ, inputX;
    private float velocityY;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _movementMode = MovementMode.Strafe;
        UpdateMouseLook();
        UpdateMovementStafe();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && MoveOrRotate == true)
        {
            MoveOrRotate = false;
            UnityEngine.Debug.Log("F");
       }
        if (Input.GetKeyDown(KeyCode.F) && MoveOrRotate == false)
        {
            MoveOrRotate = true;
        }
        //if (_movementMode == MovementMode.Platformer)
        //{
        //   MovementPlatformer();
        //}
    }

    private bool IsPlayerMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

    private void UpdateMovementStafe()
    {
        while (MoveOrRotate == true)
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
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }

    void UpdateMouseLook()
    {
        while(MoveOrRotate == false)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");

            Vector2 targetMouseDelta = new Vector2(inputX, inputZ);

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

            cameraPitch -= currentMouseDelta.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

            VertcameraPitch -= currentMouseDelta.y * VertmouseSensitivity;
            VertcameraPitch = Mathf.Clamp(VertcameraPitch, -90.0f, 90.0f);

            playerCamera.localEulerAngles = Vector3.right * VertcameraPitch;
            transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        }
        
    }

    /*private void MovementPlatformer()
    {
        if (MoveOrRotate)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;
            bool running = Input.GetKey(KeyCode.LeftShift);

            if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + _cameraRig.transform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }

            float targetSpeed = ((running) ? _runningSpeed : _walkSpeed) * inputDir.magnitude;
            _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

            velocityY += Time.deltaTime * _gravityPlatformer;
            Vector3 velocity = transform.forward * _currentSpeed + Vector3.up * velocityY;

            _controller.Move(velocity * Time.deltaTime);
            _currentSpeed = new Vector2(_controller.velocity.x, _controller.velocity.z).magnitude;

            if (_controller.isGrounded)
            {
                velocityY = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_controller.isGrounded)
                {
                    float jumpVelocity = Mathf.Sqrt(-2 * _gravityPlatformer * jumpHeight);
                    velocityY = jumpVelocity;
                }
            }
            */
    //}

}