using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class UIStats : NetworkBehaviour
{
    public Weapon weaponAmmo;

    //public GameObject character;
    //private GameObject weapon;
    public Image healthbar;
    public Text ammoDisplay;
    public float maxHealth;

    public int _currentHealth;
    public float _healthbarScale;

    private string _ammoLeft;
    private string _magSize;
    private string _ammoAll;

    private void Awake()
    {
        //character = GameObject.FindGameObjectWithTag("Player");
        //weapon = GameObject.FindGameObjectWithTag("Weapon");
        //characterHealth = character.GetComponent<PlayerCharacter>();
        //weaponAmmo = weapon.GetComponent<Weapon>();
        //_maxHealth = characterHealth.health;
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {


        //    _currentHealth = characterHealth.health;
        //    _healthbarScale = _currentHealth / _maxHealth;
        //    healthbar.fillAmount = _healthbarScale;

        //    _magSize = weaponAmmo.magazineSize.ToString();
        //    _ammoLeft = weaponAmmo.ammoCounter.ToString();
        //    ammoDisplay.text = _ammoLeft + " / " + _magSize;
    }

    public void UpdateStats(int playerHealth, Weapon playerWeapon)
    {

        _currentHealth = playerHealth;
        _healthbarScale = _currentHealth / maxHealth;
        healthbar.fillAmount = _healthbarScale;

        _magSize = playerWeapon.magazineSize.ToString();
        _ammoLeft = playerWeapon.ammoCounter.ToString();
        ammoDisplay.text = _ammoLeft + " / " + _magSize;
    }

    public void MaxHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
    }
}
