using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPoseRagdoll : MonoBehaviour
{

    protected Animator animator;
    protected WanderingAI movement;
    //protected Rigidbody rigidbody;
    // protected BoxCollider boxCollider;
    protected CharacterController characterController;

    protected Collider[] childrenCollider;
    protected Rigidbody[] childrenRigidbody;
    protected Collider[] hitBoxColliders;

    private float _bulletForce;
    private Vector3 _bulletDirection;




    protected void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<WanderingAI>();
        //rigidbody = GetComponent<Rigidbody>();
        //boxCollider = GetComponent<BoxCollider>();
        characterController = GetComponent<CharacterController>();
        childrenCollider = GetComponentsInChildren<Collider>();
        childrenRigidbody = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in childrenRigidbody)
            rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;



    }

    // Start is called before the first frame update
    void Start()
    {
        characterController.detectCollisions = false;
        RagdollActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RagdollActive(bool active)
    {
        animator.enabled = false;
        //movement.enabled = false;

        hitBoxColliders = GetComponentsInChildren<Collider>();

        //Kollar efter HitBoxen och inaktiverar vid Death
        foreach (var collider in hitBoxColliders)
        {
            if (collider.tag == "HitBox")
            {
                collider.enabled = !active;
                //Debug.Log("HitBoxcol" + collider.tag);
            }
        }

        foreach (var collider in childrenCollider)
        {
            if (collider.tag != "HitBox")
            {
                collider.enabled = active;
                //Debug.Log("RagDollcol" + collider.tag);
            }
        }

        foreach (var rigidbody in childrenRigidbody)
        {
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

        characterController.enabled = !active;

    }

    //hämtas från EnemyCharacter
    public void BulletForce(float bulletForce)
    {
        _bulletForce = bulletForce;
    }

    //hämtas från EnemyCharacter
    public void BulletVector(Vector3 bulletDirection)
    {
        _bulletDirection = bulletDirection;
    }


    public void Die()
    {
        RagdollActive(true);
        foreach (Rigidbody rigidbody in childrenRigidbody)
        {
            if (rigidbody.tag != "Weapon")
            {
                rigidbody.AddExplosionForce(_bulletForce, transform.position - _bulletDirection, 2, 0, ForceMode.Impulse);
            }
        }
    }

}
