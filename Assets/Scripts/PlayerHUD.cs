using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

	[Range(0,1)]
	public float CriticalPercentage;
	[Range(0,1)]
	public float NormalPercentage;

	public Image[] HealthbarColors;

	public PlayerController Player;
	public Sprite DefaultIcon;

	public Text PlayerName;
	public Image Portrait;
	public Slider Healthbar;

	public Image PrimaryWeaponIcon;
	public Image SecondaryWeaponIcon;

	public Image PrimaryHealingIcon;
	public Image SecondaryHealingIcon;
	public Image GrenadeIcon;

	public Slider WeaponClip;
	public Text MagStats;

	public Sprite DefaultPrimaryWeaponIcon;
	public Sprite DefaultSecondaryWeaponIcon;
	public Sprite DefaultPrimaryHealingIcon;
	public Sprite DefaultSecondaryHealingIcon;
	public Sprite DefaultGrenadeIcon;

	private readonly Color EquippedColor = new Color(1, 1, 1, 1);
	private readonly Color UnequippedColor = new Color(1, 1, 1, 0.1f);

	// Use this for initialization
	void Start () {

		PlayerName.text = Player.GetName();
		Portrait.sprite = Player.PlayerPortrait;
		Healthbar.minValue = 0;
		Healthbar.maxValue = Player.MAX_HEALTH;

		UpdateItemSlots();
	}
	
	// Update is called once per frame
	void Update () {

		UpdateHealthbar();
		UpdateItemSlots();
	}


	private void UpdateItemSlots()
	{
		string magCount = "\n∞";

		// Weapons
		if(Player.EquippedPrimaryWeapon != null)
		{
			PrimaryWeaponIcon.color = EquippedColor;
			PrimaryWeaponIcon.sprite = Player.EquippedPrimaryWeapon.Icon;
			WeaponClip.maxValue = Player.EquippedPrimaryWeapon.ClipSize;
			WeaponClip.normalizedValue = 
				Player.PrimaryWeaponBulletCount / WeaponClip.maxValue;

			MagStats.text = Player.PrimaryWeaponBulletCount + magCount;
		}
		else
		{
			PrimaryWeaponIcon.color = UnequippedColor;
			PrimaryWeaponIcon.sprite = DefaultPrimaryWeaponIcon;
			WeaponClip.normalizedValue = 0;
			MagStats.text = 0 + magCount;
		}

		if(Player.EquippedSecondaryWeapon != null)
		{
			SecondaryWeaponIcon.color = EquippedColor;
			SecondaryWeaponIcon.sprite = Player.EquippedSecondaryWeapon.Icon;
			WeaponClip.maxValue = Player.EquippedSecondaryWeapon.ClipSize;
			WeaponClip.normalizedValue =
				Player.SecondaryWeaponBulletCount / WeaponClip.maxValue;
			MagStats.text = Player.SecondaryWeaponBulletCount + magCount;
		}
		else
		{
			SecondaryWeaponIcon.color = UnequippedColor;
			SecondaryWeaponIcon.sprite = DefaultSecondaryWeaponIcon;
			WeaponClip.normalizedValue = 0;
			MagStats.text = 0 + magCount;
		}

		// Healing items
		if(Player.EquippedPrimaryHealing != null)
		{
			PrimaryHealingIcon.color = EquippedColor;
			PrimaryHealingIcon.sprite = Player.EquippedPrimaryHealing.Icon;
		}
		else
		{
			PrimaryHealingIcon.color = UnequippedColor;
			PrimaryHealingIcon.sprite = DefaultPrimaryHealingIcon;
		}

		if(Player.EquippedSecondaryHealing != null)
		{
			SecondaryHealingIcon.color = EquippedColor;
			SecondaryHealingIcon.sprite = Player.EquippedSecondaryHealing.Icon;
		}
		else
		{
			SecondaryHealingIcon.color = UnequippedColor;
			SecondaryHealingIcon.sprite = DefaultSecondaryHealingIcon;
		}

		//Grenade
		if(Player.EquippedGrenade != null)
		{
			GrenadeIcon.color = EquippedColor;
			GrenadeIcon.sprite = Player.EquippedGrenade.Icon;
		}
		else
		{
			GrenadeIcon.color = UnequippedColor;
			GrenadeIcon.sprite = DefaultGrenadeIcon;
		}
	}


	private void UpdateHealthbar()
	{
		Healthbar.normalizedValue = Player.GetPlayerNormalizedHealth();

		float life = Healthbar.normalizedValue * 100f;

		if(life >= Player.MAX_HEALTH * NormalPercentage)
		{
			ChangeHealthbarColor(new Color(0, 1, 0, 1));
		}
		else if(life < Player.MAX_HEALTH * NormalPercentage)
		{
			ChangeHealthbarColor(new Color(1, 0.64f, 0, 1));
		}

		if(life <= Player.MAX_HEALTH * CriticalPercentage)
		{
			ChangeHealthbarColor(new Color(1, 0, 0, 1));
		}

	}

	private void ChangeHealthbarColor(Color color)
	{
		foreach(Image image in HealthbarColors)
		{
			image.color = color;
		}
	}
}
