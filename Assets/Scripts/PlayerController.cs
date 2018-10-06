using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _playerNumber;
    private float _currentHealth;

	public readonly float MAX_HEALTH = 100f;

	// Unity public editor variables
    public Sprite PlayerPortrait;
	public ArmoryController Armory;

	// Properties
    public Weapon EquippedPrimaryWeapon { get; private set; }
    public Weapon EquippedSecondaryWeapon { get; private set; }
	public Grenade EquippedGrenade { get; private set; }
	public HealingItem EquippedPrimaryHealing { get; private set; }
	public HealingItem EquippedSecondaryHealing { get; private set; }
	public int PrimaryWeaponBulletCount { get; private set; }
	public int SecondaryWeaponBulletCount { get; private set; }

	public int PlayerNumber
    {
        get { return _playerNumber; }
    }

    // Use this for initialization
    void Start()
    {
        _currentHealth = MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetPlayerNormalizedHealth()
    {
        return _currentHealth / MAX_HEALTH;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
        }
    }

    public void GainHealth(float amount)
    {
        _currentHealth += amount;

        if (_currentHealth >= MAX_HEALTH)
        {
            _currentHealth = MAX_HEALTH;
        }
    }

    public string GetName()
    {
        return "Player0" + _playerNumber;
    }

	public void EquipWeapon(string name)
	{
		Weapon weapon = Armory.GetItem<Weapon>(name);
		Debug.Log(weapon.name);

		if(weapon != null)
		{
			if(weapon.WeaponType == WeaponType.Primary)
			{
				EquippedPrimaryWeapon = weapon;
				PrimaryWeaponBulletCount = weapon.ClipSize;
			}
			else
			{
				EquippedSecondaryWeapon = weapon;
				SecondaryWeaponBulletCount = weapon.ClipSize;
			}
		}
	}

	public void UnequipWeapon(WeaponType type)
	{
		if(type == WeaponType.Primary)
		{
			EquippedPrimaryWeapon = null;
			PrimaryWeaponBulletCount = 0;
		}
		else
		{
			EquippedSecondaryWeapon = null;
			SecondaryWeaponBulletCount = 0;
		}
	}

	public void FireWeapon(WeaponType type)
	{
		if(type == WeaponType.Primary && EquippedPrimaryWeapon != null)
		{
			PrimaryWeaponBulletCount--;
		}
		else if(EquippedSecondaryWeapon != null)
		{
			SecondaryWeaponBulletCount--;
		}
	}
	public void ReloadWeapon(WeaponType type)
	{
		if(type == WeaponType.Primary && EquippedPrimaryWeapon != null)
		{
			PrimaryWeaponBulletCount = EquippedPrimaryWeapon.ClipSize;
		}
		else if(EquippedSecondaryWeapon != null)
		{
			SecondaryWeaponBulletCount = EquippedSecondaryWeapon.ClipSize;
		}
	}

	public void EquipGrenade(string name)
	{
		Grenade grenade = Armory.GetItem<Grenade>(name);

		if(grenade != null)
		{
			EquippedGrenade = grenade;
		}
	}

	public void UseGrenade()
	{
		// Throw grenade;

		EquippedGrenade = null;
	}

	public void EquipHealingItem(string name)
	{
		Grenade grenade = Armory.GetItem<Grenade>(name);

		if(grenade != null)
		{
			EquippedGrenade = grenade;
		}
	}

	public void UseHealingItem()
	{
		// Throw grenade;
	
		EquippedGrenade = null;
	}
}
