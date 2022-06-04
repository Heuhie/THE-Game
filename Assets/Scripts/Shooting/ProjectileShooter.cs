using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class ProjectileShooter : NetworkBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    //private List<GameObject> _projectiles;
    //private GameObject _projectile;

    public void Fire(Vector3 location, Quaternion direction)
    {
        if (!IsClient) return;

            RequestFireServerRpc(location, direction);

        // Debug.Log("Projectile spawned");
    }

    [ServerRpc]
    void RequestFireServerRpc(Vector3 location, Quaternion direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, location, direction);
        projectile.GetComponent<NetworkObject>().Spawn();
    }
}
