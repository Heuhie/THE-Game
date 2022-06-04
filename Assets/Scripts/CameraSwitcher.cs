using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    bool _freeLook = true;
    Animator _animator;
    [SerializeField] Transform _pivot;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            if (_freeLook) {
                Aim();
            }
            else {
                FreeLook();
            }
            _freeLook = !_freeLook;
        }
        //else {
        //    _animator.Play("FreeLook");
        //    _freeLook = true;
        //}
    }

    public void Aim()
    {
        _animator.Play("Aim");
    }

    public void FreeLook()
    {
        _animator.Play("FreeLook");
    }
}
