using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using MLAPI;
using MLAPI.NetworkVariable;

public class RigController : NetworkBehaviour
{
    [SerializeField] Transform rigTarget;

    RigBuilder _rigBuilder;

    // Start is called before the first frame update
    void Start()
    {
        _rigBuilder = GetComponent<RigBuilder>();

        if (IsLocalPlayer) {
            SetAimTarget(Camera.main.transform.GetChild(0));
        }
        else {
            SetAimTarget(rigTarget);
        }
    }

    void Update()
    {

    }

    // Set aim rig targets
    public void SetAimTarget(Transform target)
    {
        WeightedTransformArray sources;

        var upperAimTarget = _rigBuilder.layers[0].rig.transform.GetChild(0).GetComponent<MultiPositionConstraint>();
        sources = upperAimTarget.data.sourceObjects;
        sources.Clear();
        sources.Add(new WeightedTransform(target, 1f));
        upperAimTarget.data.sourceObjects = sources;

        var weaponAimTarget = _rigBuilder.layers[1].rig.transform.GetChild(0).GetComponent<MultiPositionConstraint>();
        sources = weaponAimTarget.data.sourceObjects;
        sources.Clear();
        sources.Add(new WeightedTransform(target, 1f));
        weaponAimTarget.data.sourceObjects = sources;

        _rigBuilder.Build();
    }
}
