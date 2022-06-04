using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;


public class PlayerCharacter : NetworkBehaviour
{
    public List<Spawner> _spawners;

    private int _maxHealth;

    [SerializeField] Weapon weapon;
    [SerializeField] UIStats characterStats;
    [SerializeField] GameObject uiController;
    [SerializeField] int damage = 1;
    //[SerializeField] private Image _healthbar;

    protected ToggleRagdoll playerDeath;

    public NetworkVariableInt health = new NetworkVariableInt(2);

    public float BulletForce { get; set; }
    public Vector3 BulletDirection { get; set; }

    //public Weapon weaponAmmo;
    public PlayerCharacter characterHealth;


    // Start is called before the first frame update
    void Start()
    {
        uiController = GameObject.FindGameObjectWithTag("UI");
        characterStats = uiController.GetComponent<UIStats>();
        characterHealth = GetComponent<PlayerCharacter>();


        if (health.Value < 1) {
            GetComponent<ToggleRagdoll>().Die(0, Vector3.zero);
            foreach (var component in gameObject.GetComponents<MonoBehaviour>()) {
                component.enabled = false;
            }
        }

        _maxHealth = health.Value;
        characterStats.MaxHealth(_maxHealth);

        health.OnValueChanged += OnHealthChanged;

        foreach (Spawner spawner in GameObject.Find("Spawners").GetComponentsInChildren<Spawner>()) {
            _spawners.Add(spawner);
        }
    }


    void OnEnable()
    {
        //health.OnValueChanged += OnHealthChanged;
    }


    void OnDisable()
    {
        health.OnValueChanged -= OnHealthChanged;
    }


    // Called when server notifies clients of health change
    void OnHealthChanged(int oldValue, int newValue)
    {
        // Host will count as client so server does nothing here
        if (IsClient) {
            if (newValue < 1) {
                playerDeath = GetComponent<ToggleRagdoll>();
                //playerDeath.SetBulletForce(BulletForce);
                //playerDeath.SetBulletDirection(BulletDirection);
                playerDeath.Die(BulletForce, BulletDirection);
                Debug.Log("DIE");
            }


        }
    }


    public void Hurt(int damage)
    {
        if (!IsServer) return;

        health.Value -= damage;

        Debug.Log("Server Health: " + health.Value);
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }

   
    //private void OnCollisionEnter(Collision other)
    //{
    //    Debug.Log("Collided with " + other.gameObject.name);
    //    projectile = other.gameObject.GetComponent<Projectile>();
    //    if (projectile != null)
    //    {
    //        Hurt(damage);
    //        Debug.Log("Got Hit");
    //        bulletForce = projectile.projectileForce;
    //        bulletDirection = projectile.transform.forward;

    //    }
    //}


    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            if (health.Value < 1 && Input.GetKeyDown(KeyCode.G)) {
                int index = Random.Range(0, 3);
                _spawners[index].Respawn(GetComponent<NetworkObject>());
            }

            characterStats.UpdateStats(health.Value, weapon);
            if (weapon != null)
            {
                if (Input.GetButton("Fire1") && weapon.CanFire)
                {
                    weapon.Fire();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    weapon.Reload();
                }
            }
        }
    }
}
