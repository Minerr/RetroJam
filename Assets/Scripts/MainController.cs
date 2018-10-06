using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public PlayerController playerOne;
	public PlayerController playerTwo;
	public ArmoryController Armory;

	// Use this for initialization
	void Start () {
		playerOne.EquipWeapon("Pistol");
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			ToggleWeapon("Pistol", WeaponType.Secondary);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			ToggleGrenade("Molotov");
		}

		if(Input.GetKeyDown(KeyCode.Q))
		{
		}
		if(Input.GetKeyDown(KeyCode.W))
		{
			
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
			playerOne.FireWeapon(WeaponType.Secondary);
		}
		if(Input.GetKeyDown(KeyCode.R))
		{
			playerOne.ReloadWeapon(WeaponType.Secondary);
		}

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
