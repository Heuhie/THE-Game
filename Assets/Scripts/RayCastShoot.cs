using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class RayCastShoot : MonoBehaviour
{
    [SerializeField] public Transform shootDir;
    private GameObject _muzzle;
    public float _zray;
    public int damage = 1;


    // Start is called before the first frame update
    void Start()
    {
       _muzzle = GetComponent<GameObject>();
        //Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = new Ray(transform.position, shootDir.position - transform.position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {

                //See if character hit
                GameObject hitobject = hit.transform.gameObject;
                Enemy target = hitobject.GetComponent<Enemy>();
                if (target != null)
                {
                    target.Hurt(damage);
                    Debug.Log("Target hit");
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
        IEnumerator SphereIndicator(Vector3 pos)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = pos;

            yield return new WaitForSeconds(5);
            Destroy(sphere);
        }
    }
}
