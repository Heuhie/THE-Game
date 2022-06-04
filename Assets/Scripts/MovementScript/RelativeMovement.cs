using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MLAPI;

//[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : NetworkBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject camera;
    

    private float rotSpeed = 15.0f;
    private float _vertSpeed;
    private Animator _animator;
    private ControllerColliderHit _contact;
    private CharacterController _charcontroller;
    private float _maxAngle = 91f;

    public float moveSpeed = 6.0f;
    public float walkSpeed = 2.0f;
    public float gravity = -9.8f;
    public float jumpSpeed = 12.0f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public Transform characterPivot;

    // Inputs
    public Vector2 _movementInput;
    public bool _aiming;
    public bool _jumping;


    // Start is called before the first frame update
    void Start()
    {
        _vertSpeed = minFall;
        _charcontroller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        if (IsLocalPlayer) {
            target = Camera.main.transform;
            camera.SetActive(true);
    }

}

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer) {
            ProcessInput();
            RotatePlayer();
            MovePlayer();
            UpdateAnimation();
        }
    }

    void ProcessInput()
    {
        if (Input.GetButtonDown("Fire2"))
            _aiming = !_aiming;
        _movementInput.x = Input.GetAxis("Horizontal");
        _movementInput.y = Input.GetAxis("Vertical");
        _jumping = Input.GetButtonDown("Jump");
    }

    void RotatePlayer()
    {
        // Rotate player to match camera when aiming
        Quaternion cameraRotation = Quaternion.AngleAxis(target.eulerAngles.y, Vector3.up);
        if (_aiming)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraRotation, rotSpeed * Time.deltaTime);
        }
        else{
            // Rotate player when turning too far
            Quaternion deltaRotation;
            Vector2 playerXZ = new Vector2(transform.forward.x, transform.forward.z);
            Vector2 cameraXZ = new Vector2(target.forward.x, target.forward.z);
            float angle = Vector2.SignedAngle(playerXZ, cameraXZ);
            if (angle > _maxAngle) {
                deltaRotation = Quaternion.Euler(0f, _maxAngle, 0f) * cameraRotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, deltaRotation, rotSpeed * Time.deltaTime);
            }
            else if (angle < -_maxAngle) {
                deltaRotation = Quaternion.Euler(0f, -_maxAngle, 0f) * cameraRotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, deltaRotation, rotSpeed * Time.deltaTime);
            }

            // Rotate normally when moving, except backwards
            Quaternion rotation;
            Vector3 direction = new Vector3(_movementInput.x, 0f, _movementInput.y);
            if (_movementInput.x != 0 || _movementInput.y != 0) {
                rotation = (_movementInput.y < 0f) ? Quaternion.LookRotation(-direction) : Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, cameraRotation * rotation, rotSpeed * Time.deltaTime);
            }
        }
    }

    void MovePlayer()
    {
        float speed = moveSpeed;
        if (_aiming || _movementInput.y < 0f)
            speed = walkSpeed;

        // Horizontal movement
        Vector3 movement = Vector3.zero;
        if (_movementInput.x != 0 || _movementInput.y != 0) {
            movement.x = _movementInput.x * speed;
            movement.z = _movementInput.y * speed;
            movement = Vector3.ClampMagnitude(movement, speed);

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;
        }

        // Vertical movement
        bool hitGround = false;
        RaycastHit hit;
        if (_vertSpeed < 0 && Physics.Raycast(characterPivot.position, Vector3.down, out hit)) {
            float check = (_charcontroller.height + _charcontroller.radius) / 2.1f;
            hitGround = hit.distance <= check;
        }

        if (hitGround) {
            if (_jumping)
                _vertSpeed = jumpSpeed;
            else
                _vertSpeed = minFall;
        }
        else {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
                _vertSpeed = terminalVelocity;
            if (_contact != null)
                if (_charcontroller.isGrounded) {
                    if (Vector3.Dot(movement, _contact.normal) < 0)
                        movement = _contact.normal * speed;
                    else
                        movement += _contact.normal * speed;
                }
        }
        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _charcontroller.Move(movement);
    }

    void UpdateAnimation()
    {
        _animator.SetFloat("SpeedX", _movementInput.x);
        _animator.SetFloat("SpeedY", _movementInput.y);
        _animator.SetFloat("Speed", _movementInput.magnitude);
        _animator.SetBool("Aiming", _aiming);
        //_animator.SetBool("Jumping", true);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }
}
