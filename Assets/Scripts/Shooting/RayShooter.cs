using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Fire(Vector3 location, Vector3 direction)
    {
        StartCoroutine(SpawnProjectile(location, direction));
        Debug.Log("Projectile spawned");
    }

    private IEnumerator SpawnProjectile(Vector3 position, Vector3 direction)
    {
        if (Physics.Raycast(position, direction, out RaycastHit hit)) {

            StartCoroutine(ScanIndicator(position, hit.point));
            StartCoroutine(SphereIndicator(hit.point));
        }

        yield return new WaitForSeconds(1);
    }

    private IEnumerator SphereIndicator(Vector3 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }

    private IEnumerator ScanIndicator(Vector3 origin, Vector3 end)
    {
        Debug.DrawLine(origin, end, Color.red, 1);

        yield return null;
    }
}
