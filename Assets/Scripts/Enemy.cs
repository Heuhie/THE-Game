using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 5;
    protected ToggleRagdoll death;
    


    public void Hurt(int damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);
        if (health < 1)
        {
            death = GetComponent<ToggleRagdoll>();
            death.Die(0f, Vector3.zero);
            Debug.Log("DIE");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
