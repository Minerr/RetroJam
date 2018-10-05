using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject {

	public readonly float maxHealth;
	public float currentHealth;
	public readonly Sprite playerImage;
	public Weapon equippedWeapon;
	public Grenade equippedGrenade;
	public SpecialItem equippedSpecialItem;
	public HealingItem equippedItem;
}
