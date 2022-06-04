using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Projectile : NetworkBehaviour
{
    
    public float projectileSpeed = 10f;
    public float projectileForce = 3f;
    public float lifeTime = 10f;

    private float _timer;
    private Rigidbody _body;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
        _body = GetComponent<Rigidbody>();
        _body.velocity = transform.forward * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer) {
            if (_timer >= lifeTime) {
                Destroy(this.gameObject);
                return;
            }

            _timer += Time.deltaTime;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    StartCoroutine(SphereIndicator(collision.GetContact(0).point));
    //}


    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        SphereCollider sc = sphere.GetComponent<SphereCollider>();
        sphere.transform.localScale = sphere.transform.localScale / 2;
        sc.enabled = false;
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }


}
