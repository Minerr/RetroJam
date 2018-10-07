using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private int _playerNumber;
	private float _currentHealth;
	private int _primaryBulletCount = 0;
	private int _secondaryBulletCount = 0;
	private int _primaryMagCount = 0;
	private int _secondaryMagCount = 0;
	private WeaponType _currentWeaponType = WeaponType.Secondary;
	private bool _canShootPrimary = true;
	private bool _canShootSecondary = true;
	private float cooldown = 0f;
	public GameObject bullet;
	private bool shotLastFrame;
	

	public readonly float MAX_HEALTH = 100f;

	// Unity public editor variables
	public Sprite PlayerPortrait;
	public ArmoryController Armory;
	public GameObject PlayerArm;

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

	    if (Input.GetButtonDown("ResetLevel"))
	    {
	        SceneManager.LoadScene("Main");

	    }
		//if(Input.GetButtonDown("UseGrenade"))
		//{
		//	UseGrenade();
		//}

		if(Input.GetAxisRaw("Fire1") < 0.5)
		{
			shotLastFrame = false;
		}

		if(GetCurrentWeapon().AutoFire)
		{
			if(Input.GetAxisRaw("Fire1") > 0.5)
			{
				FireWeapon();
			}
		}
		else
		{
			if(Input.GetAxisRaw("Fire1") > 0.5 && shotLastFrame == false)
			{
				shotLastFrame = true;
				FireWeapon();
			}
		}

		if(Input.GetButtonDown("Reload"))
		{
			StartCoroutine(ReloadWeapon());
		}
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

	public void FireWeapon()
	{
		Weapon weapon = GetCurrentWeapon();

		if(CanShootCurrentWeapon())
		{
			if(Time.time >= cooldown)
			{
				cooldown = Time.time + 1f / weapon.FireRate;
				if(weapon.BulletsPerShot > 1)
				{
					MultiShot();
				}
				else
				{
					ShootBullet(PlayerArm.transform.rotation);
				}

				RemoveBulletFromChamber();
				if(CurrentWeaponBulletCount <= 0)
				{
					LockWeapon(CurrentWeaponType);
				}
			}
		}
	}

	private void RemoveBulletFromChamber()
	{
		if(CurrentWeaponType == WeaponType.Primary && EquippedPrimaryWeapon != null)
		{
			_primaryBulletCount--;
		}
		else if(EquippedSecondaryWeapon != null)
		{
			_secondaryBulletCount--;
		}
	}

	private  void MultiShot()
	{
		Weapon weapon = GetCurrentWeapon();

		for(int i = 0; i < weapon.BulletsPerShot; i++)
		{
			float vinkel = (weapon.BulletSpread / 2) - (weapon.BulletSpread / (weapon.BulletsPerShot - 1)) * i;

			Quaternion angle = Quaternion.Euler(0, 0, vinkel);
			var angleOffset = PlayerArm.transform.rotation * angle;

			ShootBullet(angleOffset);
		}
	}

	private void ShootBullet(Quaternion angle)
	{
		Weapon weapon = GetCurrentWeapon();

		float weaponAccuracy = Random.Range(-1 + weapon.Accuracy, 1 - weapon.Accuracy);
		Quaternion angleOffset = Quaternion.Euler(0, 0, weaponAccuracy * 20);

		var test = new Vector3(weapon.BulletSpawnPoint.x, weapon.BulletSpawnPoint.y, 0);
		var spawnPoint = PlayerArm.transform.position + PlayerArm.transform.TransformDirection(test);
		var bulletObject = Instantiate(bullet, spawnPoint, angle * angleOffset);

		var bulletScript = bulletObject.GetComponent<BulletBehavior>();
		bulletScript.Damage = weapon.DamagePerBullet;
		bulletScript.Speed = weapon.BulletSpeed;
		bulletScript.lifeTime = weapon.BulletLife;
		bulletScript.SpriteRenderer.sprite = weapon.BulletSprite;
	}

	public IEnumerator ReloadWeapon()
	{
		Debug.Log("Reloading...");
		WeaponType weaponType = CurrentWeaponType;
		LockWeapon(weaponType);
		yield return new WaitForSeconds(GetCurrentWeapon().ReloadSpeed);

		Debug.Log("Done Reloading");
		UnlockWeapon(weaponType);

		if(weaponType == WeaponType.Primary && EquippedPrimaryWeapon != null)
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

	private Weapon GetCurrentWeapon()
	{
		return (CurrentWeaponType == WeaponType.Primary) 
			? EquippedPrimaryWeapon : EquippedSecondaryWeapon;
	}

	private bool CanShootCurrentWeapon()
	{
		if(CurrentWeaponType == WeaponType.Primary)
		{
			return _canShootPrimary;
		}

		return _canShootSecondary;
	}

	private void LockWeapon(WeaponType weaponSlot)
	{
		if(weaponSlot == WeaponType.Primary)
		{
			_canShootPrimary = false;
		}
		else
		{
			_canShootSecondary = false;
		}
	}

	private void UnlockWeapon(WeaponType weaponSlot)
	{
		if(weaponSlot == WeaponType.Primary)
		{
			_canShootPrimary = true;
		}
		else
		{
			_canShootSecondary = true;
		}
	}
}
