using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class NetworkPositionOverride : NetworkBehaviour
{
    [SerializeField] Transform source;
    // Start is called before the first frame update
    void Start()
    {
        if (IsLocalPlayer) {
            source = Camera.main.transform.GetChild(0);
            Debug.Log("spawned local target");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer) {
            transform.position = source.position;
        }
    }
}
