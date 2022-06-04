using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour {

    [Header("Mouse Properties")]
    [Range(0f, 10f)]public float sensitivity = 0.5f;
    public bool useSmoothing = false;
    public float rotationSmooth = 20f;

    [Header("Angular Limits")]
    public float minAngleVertical = -45.0f;
    public float maxAngleVertical = 45.0f;

    [Header("Side-Switch Properties")]
    public float smoothingFactor = 4f;

    [Header("Aim properties")]
    public float zoomValue = 1f;

    [Header("Camera Input")]
    //public InputAction inputCamera;

    private float _rotationY;
    private float _rotationX;
    private Vector3 _forward;


    void Start()
    {
        _rotationY = transform.eulerAngles.y;
        _rotationX = transform.eulerAngles.x;
        _forward = transform.forward;
    }

    private void LateUpdate()
    {
        //float horizontalInput = inputCamera.ReadValue<Vector2>().x;
        //float verticalInput = inputCamera.ReadValue<Vector2>().y;
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");
        Quaternion q = transform.rotation;

        // Compute rotation
        if (horizontalInput != 0 || verticalInput != 0) {
            _rotationY += horizontalInput * sensitivity;
            _rotationX -= verticalInput * sensitivity;

            _rotationX = Mathf.Clamp(_rotationX, minAngleVertical, maxAngleVertical);
        }

        Quaternion rotation = Quaternion.Euler(_rotationX, _rotationY, 0);

        // Camera smoothing
        if (useSmoothing) {
            rotation = Quaternion.Slerp(q, rotation, rotationSmooth * Time.deltaTime);
        }

        transform.forward = rotation * _forward;
    }

    //private void OnEnable()
    //{
    //    inputCamera.Enable();
    //}
    //private void OnDisable()
    //{
    //    inputCamera.Disable();
    //}

}
