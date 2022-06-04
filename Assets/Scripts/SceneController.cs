using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;
    public int spawnMinX = 5;
    public int spawnMaxX = 5;
    public int spawnMinZ = 5;
    public int spawnMaxZ = 5;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SystemInfo.supportsComputeShaders);
    }

    // Update is called once per frame
    void Update()
    {
        if(_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = new Vector3(Random.Range(spawnMinX,spawnMaxX), 0, Random.Range(spawnMinZ, spawnMaxZ));
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
        }
    }
}
