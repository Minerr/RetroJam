using System;
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
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("I Am Dead");
    }

    public void GainHealth(float percentage)
    {
		percentage = Mathf.Clamp(percentage, 0, MAX_HEALTH);
		float lifegain = MAX_HEALTH * (percentage / 100);
		_currentHealth = Mathf.Clamp(_currentHealth + lifegain, 0, MAX_HEALTH);
	}

    public string GetName()
    {
        return "Player0" + _playerNumber;
    }

	public void EquipWeapon(string name)
	{
		Weapon weapon = Armory.GetItem<Weapon>(name);

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
		HealingItem item = Armory.GetItem<HealingItem>(name);

		if(item != null)
		{
			if(item.HealingType == HealingType.Primary)
			{
				EquippedPrimaryHealing = item;
			}
			else
			{
				EquippedSecondaryHealing = item;
			}
		}
	}

	public void UseHealingItem(HealingType type)
	{
		if(type == HealingType.Primary && 
			EquippedPrimaryHealing != null)
		{
			GainHealth(EquippedPrimaryHealing.HealingPercentage);
			EquippedPrimaryHealing = null;
		}
		else if(EquippedSecondaryHealing != null)
		{
			GainHealth(EquippedSecondaryHealing.HealingPercentage);
			EquippedSecondaryHealing = null;
		}
	}


	public void EquipItem(Obtainable item)
	{
		Type itemType = item.GetType();

		if(itemType == typeof(Weapon))
		{
			EquipWeapon(item.name);
		}
		else if(itemType == typeof(HealingItem))
		{
			EquipHealingItem(item.name);
		}
		else if(itemType == typeof(Grenade))
		{
			EquipGrenade(item.name);
		}
	}
}
