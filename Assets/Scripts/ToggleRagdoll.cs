using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class ToggleRagdoll : NetworkBehaviour
{
    public GameObject debug;
    public bool Active => _active;

    protected Animator animator;
    protected RelativeMovement movement;
    //protected Rigidbody rigidbody;
    // protected BoxCollider boxCollider;
    protected CharacterController characterController;

    protected Collider[] childrenCollider;
    protected Rigidbody[] childrenRigidbody;
    protected Collider[] hitBoxColliders;

    //private float _bulletForce;
    //private Vector3 _bulletDirection;
    private bool _active;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<RelativeMovement>();
        //Debug.Log("DEBUG " + movement.name);

        characterController = GetComponent<CharacterController>();

        childrenCollider = GetComponentsInChildren<Collider>();
        childrenRigidbody = GetComponentsInChildren<Rigidbody>();

        characterController.detectCollisions = false;
        RagdollActive(false);
    }


    public void RagdollActive(bool active)
    {
        _active = active;
        movement.enabled = !active;
        animator.enabled = !active;

        hitBoxColliders = GetComponentsInChildren<Collider>();

        //Kollar efter HitBoxen och inaktiverar vid Death
        foreach (var collider in hitBoxColliders)
        {
            //if (collider.tag == "HitBox")
            {
                collider.enabled = !active;
                //Debug.Log("HitBoxcol" + collider.tag);
            }
        }

        foreach (var collider in childrenCollider)
            collider.enabled = active;
        foreach (var rigidbody in childrenRigidbody)
        {
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

        characterController.enabled = !active;

        //rigidbody.detectCollisions = !active;
        // rigidbody.isKinematic = active;
        //boxCollider.enabled = !active;
    }

    public void Die(float force, Vector3 direction)
    {
        RagdollActive(true);
        foreach (Rigidbody rigidbody in childrenRigidbody)
        {
            if (rigidbody.tag != "Weapon")
            {
                rigidbody.AddExplosionForce(force, transform.position - direction, 2, 0, ForceMode.Impulse);
            }
        }
    }


}
