using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAmongUS : MonoBehaviour

{
    public GameObject targetFollow;
    private Animator _animator;
    public float speed = 3;
    //private WanderingAI _wanderingAI;




    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        //_wanderingAI = targetFollow.GetComponent<WanderingAI>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, targetFollow.transform.position, 20);

        _animator.SetFloat("Speed", speed);
    }
}
