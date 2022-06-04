using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;

public class Spawner : NetworkBehaviour
{
    [SerializeField] GameObject playerPrefab;
    public GameObject _playerPrefab => playerPrefab;


    public void Respawn(NetworkObject player)
    {
        if (!IsClient) return;

        RespawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    void RespawnPlayerServerRpc(ulong clientId)
    {
        Debug.Log("Server received respawn request");
        NetworkObject player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        ulong objectId = player.NetworkObjectId;
        foreach (var component in player.GetComponents<MonoBehaviour>()) {
            component.enabled = false;
        }

        //int index = Random.Range(0, spawners.Length);
        NetworkObject newPlayer = Instantiate(
            _playerPrefab, 
            transform.position, 
            transform.rotation
            ).GetComponent<NetworkObject>();
        newPlayer.SpawnAsPlayerObject(clientId);

        RespawnPlayerClientRpc(objectId);
    }

    [ClientRpc]
    void RespawnPlayerClientRpc(ulong objectId)
    {
        Debug.Log("Client received server respawn cleanup request");
        NetworkObject player = NetworkSpawnManager.SpawnedObjects[objectId];
        foreach (var component in player.GetComponents<MonoBehaviour>()) {
            component.enabled = false;
        }
    }
}
