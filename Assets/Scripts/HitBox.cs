using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject hitBox;
    public PhysicMaterial hitBoxMaterial;
    public float bounce = 1;
    public float bulletDynamicFriction = 0;
    public float bulletStaticFriction = 0;

    //private Collider hitBoxCollider;
    private GameObject _parent;
    private EnemyCharacter enemyCharacter;
    private PlayerCharacter playerCharacter;
    private TPoseCharacter tPoseCharacter;
    

    public void Start()
    {
        //PhysicMaterial hitBoxMaterial = new PhysicMaterial();
        //hitBoxMaterial.dynamicFriction = bulletDynamicFriction;
        //hitBoxMaterial.staticFriction = bulletStaticFriction;
        //hitBoxMaterial.bounciness = bounce;

        

        enemyCharacter = transform.GetComponentInParent<EnemyCharacter>();
        if(enemyCharacter == null)
        {
            playerCharacter = transform.GetComponentInParent<PlayerCharacter>();
            if(playerCharacter == null)
            {
                tPoseCharacter = transform.GetComponentInParent<TPoseCharacter>();
            }
        }

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders)
        {
            _parent = collider.gameObject;
            //Debug.Log(_parent.name);
            //hitBox = new GameObject("HitBox");
            //hitBox.transform.SetParent(_parent.transform, false);
            //hitBox.transform = _parent.transform;

            //hitBox.tag = "HitBox";
            //GameObject bodypart = collider.gameObject;

            if(collider.GetType() == typeof(BoxCollider) && collider.tag != "Weapon")
            {
                BoxCollider _boxCollider = collider as BoxCollider;
                Vector3 _colliderSize = _boxCollider.size;
                Vector3 _colliderCenter = _boxCollider.center;
                hitBox = new GameObject("HitBox");
                hitBox.transform.SetParent(_parent.transform, false);
                hitBox.tag = "HitBox";
                hitBox.AddComponent<Rigidbody>();
                Rigidbody rigidbody = hitBox.GetComponent<Rigidbody>();
                rigidbody.isKinematic = true;
                rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                hitBox.AddComponent<BoxCollider>();
                BoxCollider _hitBoxCollider = hitBox.GetComponent<BoxCollider>();
                _hitBoxCollider.size = _colliderSize;
                _hitBoxCollider.center = _colliderCenter;
                //_hitBoxCollider.material = hitBoxMaterial;

                if (enemyCharacter)
                {
                    hitBox.AddComponent<EnemyHitDetection>();
                }
                else if (playerCharacter)
                {
                    hitBox.AddComponent<PlayerHitDetection>();
                }
                else if (tPoseCharacter)
                {
                    hitBox.AddComponent<TPoseHitDetection>();
                }
            }
            else if(collider.GetType() == typeof(CapsuleCollider))
            {
                CapsuleCollider _capsuleCollider = collider as CapsuleCollider;
                float _colliderHeight = _capsuleCollider.height;
                float _colliderRadius = _capsuleCollider.radius;
                Vector3 _colliderCenter = _capsuleCollider.center;
                hitBox = new GameObject("HitBox");               
                hitBox.transform.SetParent(_parent.transform, false);
                hitBox.tag = "HitBox";
                hitBox.AddComponent<Rigidbody>();
                Rigidbody rigidbody = hitBox.GetComponent<Rigidbody>();
                rigidbody.isKinematic = true;
                rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                hitBox.AddComponent<CapsuleCollider>();
                CapsuleCollider _hitBoxCollider = hitBox.GetComponent<CapsuleCollider>();
                _hitBoxCollider.height = _colliderHeight;
                _hitBoxCollider.radius = _colliderRadius;
                _hitBoxCollider.center = _colliderCenter;
                _hitBoxCollider.material = hitBoxMaterial;

                if (enemyCharacter || tPoseCharacter)
                {
                    hitBox.AddComponent<EnemyHitDetection>();
                }
                else if (playerCharacter)
                {
                    hitBox.AddComponent<PlayerHitDetection>();
                }
                else if (tPoseCharacter)
                {
                    hitBox.AddComponent<TPoseHitDetection>();
                }
            }
            else
            {
                //Debug.Log("got here");
            }
           
           
        }
    }

}
