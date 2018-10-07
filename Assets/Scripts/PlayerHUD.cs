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

	public Image CurrentWeaponIcon_Primary;
	public Image CurrentWeaponIcon_Secondary;

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

	private readonly Color FullyTransparent = new Color(1, 1, 1, 0);
	private readonly Color FullyOpaque = new Color(1, 1, 1, 1);
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

	private void OnGUI()
	{
		UpdateHealthbar();
		UpdateItemSlots();
	}


	private void UpdateItemSlots()
	{
		if(Player.CurrentWeaponType == WeaponType.Primary)
		{
			CurrentWeaponIcon_Primary.color = FullyOpaque;
			CurrentWeaponIcon_Secondary.color = FullyTransparent;
		}
		else
		{
			CurrentWeaponIcon_Secondary.color = FullyOpaque;
			CurrentWeaponIcon_Primary.color = FullyTransparent;
		}

		// Display Magazine stats
		string magCount = "\n";
		magCount += "∞";
		//magCount += Player.CurrentWeaponMagCount;

		WeaponClip.maxValue = Player.CurrentWeaponClipSize;
		WeaponClip.normalizedValue =
			Player.CurrentWeaponBulletCount / WeaponClip.maxValue;

		MagStats.text = Player.CurrentWeaponBulletCount + magCount;

		// Weapons
		if(Player.EquippedPrimaryWeapon != null)
		{
			PrimaryWeaponIcon.color = FullyOpaque;
			PrimaryWeaponIcon.sprite = Player.EquippedPrimaryWeapon.Icon;
		}
		else
		{
			PrimaryWeaponIcon.color = UnequippedColor;
			PrimaryWeaponIcon.sprite = DefaultPrimaryWeaponIcon;
		}

		if(Player.EquippedSecondaryWeapon != null)
		{
			SecondaryWeaponIcon.color = FullyOpaque;
			SecondaryWeaponIcon.sprite = Player.EquippedSecondaryWeapon.Icon;
		}
		else
		{
			SecondaryWeaponIcon.color = UnequippedColor;
			SecondaryWeaponIcon.sprite = DefaultSecondaryWeaponIcon;
		}

		// Healing items
		if(Player.EquippedPrimaryHealing != null)
		{
			PrimaryHealingIcon.color = FullyOpaque;
			PrimaryHealingIcon.sprite = Player.EquippedPrimaryHealing.Icon;
		}
		else
		{
			PrimaryHealingIcon.color = UnequippedColor;
			PrimaryHealingIcon.sprite = DefaultPrimaryHealingIcon;
		}

		if(Player.EquippedSecondaryHealing != null)
		{
			SecondaryHealingIcon.color = FullyOpaque;
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
			GrenadeIcon.color = FullyOpaque;
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
