using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;

public class MultiplayerController : NetworkBehaviour
{
    [SerializeField] Spawner[] spawners;
    [SerializeField] NetworkObject playerPrefab;

    public void Update()
    {
        
    }


    public void OnClickHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(Vector3.zero, Quaternion.identity);
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == "SecretPassword";
        callback(true, null, approve, Vector3.zero, Quaternion.identity);
    }

    public void OnClickJoin()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("SecretPassword");
        NetworkManager.Singleton.StartClient();
    }

    Vector3 RandomSpawn()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        return new Vector3(x, y, z);
    }

}
