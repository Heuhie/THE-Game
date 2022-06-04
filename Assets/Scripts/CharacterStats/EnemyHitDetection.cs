using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyHitDetection : MonoBehaviour
{
    public EnemyCharacter enemyCharacter;
    public int damage = 1;
    public float bulletForce;
    public Vector3 bulletDirection;
    public Projectile projectile;
    public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        enemyCharacter = GetComponentInParent<EnemyCharacter>();
        characterController = GetComponentInParent<CharacterController>();
        

    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collided with " + other.gameObject.name);
        projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            enemyCharacter.Hurt(damage);
            Debug.Log("EnemyCharacter Got Hit");
            enemyCharacter.bulletForce = projectile.projectileForce;
            enemyCharacter.bulletDirection = projectile.transform.forward;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
