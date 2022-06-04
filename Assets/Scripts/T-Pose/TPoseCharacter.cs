using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPoseCharacter : MonoBehaviour
{
    public int health = 5;
    protected TPoseRagdoll enemyDeath;
    public int damage = 1;
    public float bulletForce;
    public Vector3 bulletDirection;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Applyar skada samt triggar ragdoll om ingen hälsa kvar
    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);

        if (health < 1)
        {
            enemyDeath = GetComponent<TPoseRagdoll>();
            enemyDeath.BulletForce(bulletForce);
            enemyDeath.BulletVector(bulletDirection);
            enemyDeath.Die();
            Debug.Log("DIE");
        }
    }

    public void BulletForce(float _bulletForce)
    {
        bulletForce = _bulletForce;
    }

    public void BulletDirection(Vector3 _bulletDirection)
    {
        bulletDirection = _bulletDirection;
    }

    //Kollar om collision är en projectile, hämtar projektilens force och direction samt applyar skada
    //private void OnCollisionEnter(Collision other)
    //{
    //    Debug.Log("Collided with " + gameObject.name);
    //    projectile = other.gameObject.GetComponent<Projectile>();
    //    if ( projectile != null)
    //    {
    //        Hurt(damage);
    //        Debug.Log("Enemy hit");
    //        bulletForce = projectile.projectileForce;
    //        bulletDirection = projectile.transform.forward;

    //    }
    //}

}
