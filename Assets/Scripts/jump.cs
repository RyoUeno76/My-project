using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class jump : MonoBehaviour
{
    private CharacterController _charactercontroller;
    private Vector3 _playerVelocity;
    bool inair;
    [SerializeField] private float jumpheight = 5.0f;
    private float Gravity = -9.81f;
    private bool _jump;
    bool _groundedplayer;
    // Start is called before the first frame update
    void Start()
    {
        _charactercontroller = GetComponent<CharacterController>();
        inair = false;
        Movement();
    }

    void Movement()
    {
        _groundedplayer = _charactercontroller.isGrounded;
        if (_groundedplayer)
        {
            _playerVelocity.y = 0.0f;
        }
        if(_jump && _groundedplayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpheight * -1.0f * Gravity);
            _jump = false;
        }
        _playerVelocity.y += Gravity += Time.deltaTime;
        _charactercontroller.Move(_playerVelocity * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(_charactercontroller.velocity.y == 0)
            {
                Debug.Log("Jump");
                _jump = true;
            } else
            {
                Debug.Log("Cant Jump");
            }
        }
    }


}
