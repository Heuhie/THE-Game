using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int magazineSize = 20;
    public float reloadSpeed = 2f;
    public float fireRate = 240f; // rounds per minute
    public int projectiles = 1;
    public int ammoPerShot = 1;
    public float spreadAngle = 5f;
    public float deviation = 0.5f;
    public float ammoCounter;

    public bool debugLine = false;

    [SerializeField] Transform exitPoint;

    private ProjectileShooter _firingMechanism;
    private float _cooldown;
    private float _shotTimer;


    void Start()
    {
        //_firingMechanism = GetComponent<RayShooter>();
        _firingMechanism = GetComponent<ProjectileShooter>();
        _cooldown = 0f;
        ammoCounter = magazineSize;

        _shotTimer = 60f / fireRate;
    }

    void Update()
    {
        if (_cooldown > 0f) {
            _cooldown = Mathf.Max(0f, _cooldown - Time.deltaTime);
        }
        if (debugLine) {
            
        }
    }

    public bool CanFire => (_cooldown <= 0f) && (ammoCounter > 0);

    public Quaternion ComputeFireRotation(Quaternion baseRotation)
    {
        float x, y, z;

        x = Random.Range(-spreadAngle, spreadAngle);
        y = Random.Range(-spreadAngle, spreadAngle);
        z = Random.Range(-spreadAngle, spreadAngle);
        //Debug.Log("Rotation: x: " + x + ", y: " + y + ", z: " + z);
        Quaternion rotation = Quaternion.Euler(x,y,z);

        return baseRotation * rotation;
    }

    public void Fire()
    {
        if (_cooldown <= 0f && ammoCounter > 0) {
            for (int i = 0; i < projectiles; i++) {
                //_firingMechanism.Fire(exitPoint.position, exitPoint.rotation);
                _firingMechanism.Fire(exitPoint.position, ComputeFireRotation(exitPoint.rotation));
            }
            _cooldown = _shotTimer;
            ammoCounter -= ammoPerShot;
        }
    }

    public void Reload()
    {
        StartCoroutine(TimedReload());
    }

    IEnumerator TimedReload()
    {
        ammoCounter = 0;
        yield return new WaitForSeconds(reloadSpeed);
        ammoCounter = magazineSize;
    }
}
