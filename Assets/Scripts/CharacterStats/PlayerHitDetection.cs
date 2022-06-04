using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour
{
    private PlayerCharacter playerCharacter;

    public int damage = 1;
    public float bulletForce;
    public Vector3 bulletDirection;
    public Projectile projectile;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponentInParent<PlayerCharacter>();
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log(gameObject.name + " collided with " + other.gameObject.name);
        projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            playerCharacter.Hurt(damage);
            Debug.Log("Got Hit");
            playerCharacter.BulletForce = projectile.projectileForce;
            playerCharacter.BulletDirection = projectile.transform.forward;

        }
    }
}
