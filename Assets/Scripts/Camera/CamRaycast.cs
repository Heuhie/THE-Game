using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRaycast : MonoBehaviour
{
    public float rayStart = 5f;
    public float rayDistance = Mathf.Infinity;
    public float defaultDistance = 10f;

    [SerializeField] Transform target;


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayStart * transform.forward, transform.forward, out hit)) {
            target.position = hit.point;
        }
        else {
            target.position = transform.position + defaultDistance * transform.forward;
        }
    }
}
