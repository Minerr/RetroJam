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

	private int _primaryBulletCount = 0;
	private int _secondaryBulletCount = 0;
	private int _primaryMagCount = 0;
	private int _secondaryMagCount = 0;
	private WeaponType _currentWeaponType = WeaponType.Secondary;

	// Unity public editor variables
	public Sprite PlayerPortrait;
	public ArmoryController Armory;

	// Properties
	public Weapon EquippedPrimaryWeapon { get; private set; }
	public Weapon EquippedSecondaryWeapon { get; private set; }
	public Grenade EquippedGrenade { get; private set; }
	public HealingItem EquippedPrimaryHealing { get; private set; }
	public HealingItem EquippedSecondaryHealing { get; private set; }
	public int CurrentWeaponBulletCount
	{
		get
		{
			if(CurrentWeaponType == WeaponType.Primary)
			{
				return _primaryBulletCount;
			}

			return _secondaryBulletCount;
		}
	}
	public int CurrentWeaponMagCount { get; private set; }
	public int CurrentWeaponClipSize
	{
		get
		{
			if(_currentWeaponType == WeaponType.Primary)
			{
				return EquippedPrimaryWeapon?.ClipSize ?? 0;
			}

			return EquippedSecondaryWeapon?.ClipSize ?? 0;
		}
	}

	public WeaponType CurrentWeaponType
	{
		get { return _currentWeaponType; }
		private set
		{
			if(value == WeaponType.Primary)
			{
				CurrentWeaponMagCount = _primaryMagCount;
			}
			else
			{
				CurrentWeaponMagCount = _secondaryMagCount;
			}

			_currentWeaponType = value;
		}
	}

	public int PlayerNumber
	{
		get { return _playerNumber; }
	}

	// Use this for initialization
	private void Awake()
	{
		_currentHealth = MAX_HEALTH;
	}

	void Start()
	{
		EquipWeapon("Pistol");
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetAxis("UseHealing") < 0)
		{
			UseHealingItem(HealingType.Primary);
		}
		if(Input.GetAxis("UseHealing") > 0)
		{
			UseHealingItem(HealingType.Secondary);
		}
		if(Input.GetButtonDown("SwapWeapon"))
		{
			SwapWeapon();
		}
		//if(Input.GetButtonDown("UseGrenade"))
		//{
		//	UseGrenade();
		//}
	}

	public float GetPlayerNormalizedHealth()
	{
		return _currentHealth / MAX_HEALTH;
	}

	public void TakeDamage(float amount)
	{
		_currentHealth -= amount;

		if(_currentHealth <= 0)
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
				_primaryBulletCount = weapon.ClipSize;
				CurrentWeaponType = WeaponType.Primary;
			}
			else
			{
				EquippedSecondaryWeapon = weapon;
				_secondaryBulletCount = weapon.ClipSize;
				CurrentWeaponType = WeaponType.Secondary;
			}
		}
	}

	public void UnequipWeapon(WeaponType type)
	{
		if(type == WeaponType.Primary)
		{
			EquippedPrimaryWeapon = null;
			_primaryBulletCount = 0;
			CurrentWeaponType = WeaponType.Secondary;
		}
		else
		{
			EquippedSecondaryWeapon = null;
			_secondaryBulletCount = 0;
			CurrentWeaponType = WeaponType.Primary;
		}
	}

	public void FireWeapon(WeaponType type)
	{
		if(type == WeaponType.Primary && EquippedPrimaryWeapon != null)
		{
			_primaryBulletCount--;
		}
		else if(EquippedSecondaryWeapon != null)
		{
			_secondaryBulletCount--;
		}
	}
	public void ReloadWeapon(WeaponType type)
	{
		if(type == WeaponType.Primary && EquippedPrimaryWeapon != null)
		{
			_primaryBulletCount = EquippedPrimaryWeapon.ClipSize;
		}
		else if(EquippedSecondaryWeapon != null)
		{
			_secondaryBulletCount = EquippedSecondaryWeapon.ClipSize;
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

	private void SwapWeapon()
	{
		if(EquippedPrimaryWeapon != null)
		{
			CurrentWeaponType = CurrentWeaponType == WeaponType.Primary ? WeaponType.Secondary : WeaponType.Primary;
		}
	}
}
