using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPoseHitDetection : MonoBehaviour
{
    public TPoseCharacter tPoseCharacter;
    public int damage = 1;
    public float bulletForce;
    public Vector3 bulletDirection;
    public Projectile projectile;
    public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        tPoseCharacter = GetComponentInParent<TPoseCharacter>();
        characterController = GetComponentInParent<CharacterController>();


    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collided with " + other.gameObject.name);
        projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            tPoseCharacter.Hurt(damage);
            Debug.Log("TPoseCharacter Got Hit");
            tPoseCharacter.bulletForce = projectile.projectileForce;
            tPoseCharacter.bulletDirection = projectile.transform.forward;

        }
    }

}
