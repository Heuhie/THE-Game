using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    protected ToggleRagdoll testDeath;
    protected EnemyToggleRagdoll enemyDeath;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            testDeath = GetComponent<ToggleRagdoll>();
            if (testDeath)
            {
                if (testDeath.Active)
                    testDeath.Die(GetComponent<RelativeMovement>().moveSpeed, -GetComponent<RelativeMovement>()._movementInput);
                else
                    testDeath.RagdollActive(false);
                //testDeath.Die(0f, Vector3.zero);
            }
            else if(testDeath == null)
            {
                enemyDeath = GetComponent<EnemyToggleRagdoll>();
                enemyDeath.Die();
            }
        }
    }
}
