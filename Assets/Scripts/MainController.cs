using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public PlayerController playerOne;
	public PlayerController playerTwo;
	public ArmoryController Armory;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update()
	{
		//if(Input.GetKeyDown(KeyCode.Alpha1))
		//{
		//	ToggleWeapon("Pistol", WeaponType.Secondary);
		//}
		//if(Input.GetKeyDown(KeyCode.Alpha2))
		//{
		//	ToggleGrenade("Molotov");
		//}
		//if(Input.GetKeyDown(KeyCode.Alpha3))
		//{
		//	ToggleHealingItem("HealthPack", HealingType.Primary);
		//}
		//if(Input.GetKeyDown(KeyCode.Alpha4))
		//{
		//	ToggleHealingItem("Painkiller", HealingType.Secondary);
		//}

		//if(Input.GetKeyDown(KeyCode.Q))
		//{
		//	playerOne.TakeDamage(10);
		//}
		//if(Input.GetKeyDown(KeyCode.W))
		//{

		//}
		//if(Input.GetKeyDown(KeyCode.E))
		//{
		//	playerOne.FireWeapon(WeaponType.Secondary);
		//}
		//if(Input.GetKeyDown(KeyCode.R))
		//{
		//	playerOne.ReloadWeapon(WeaponType.Secondary);
		//}

	}

	private void ToggleWeapon(string name, WeaponType type)
	{
		if(type == WeaponType.Primary)
		{
			if(playerOne.EquippedPrimaryWeapon == null)
			{
				playerOne.EquipWeapon(name);
			}
			else
			{
				playerOne.UnequipWeapon(type);
			}
		}
		else
		{
			playerOne.EquipWeapon("Pistol");
		}
	}

	private void ToggleHealingItem(string name, HealingType type)
	{
		if(type == HealingType.Primary)
		{
			if(playerOne.EquippedPrimaryHealing == null)
			{
				playerOne.EquipHealingItem(name);
			}
			else
			{
				playerOne.UseHealingItem(type);
			}
		}
	}

	private void ToggleGrenade(string name)
	{
		if(playerOne.EquippedGrenade == null)
		{
			playerOne.EquipGrenade(name);
		}
		else
		{
			playerOne.UseGrenade();
		}
	}
}
