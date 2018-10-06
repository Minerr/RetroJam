using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

	public PlayerController Player;
	public Sprite DefaultIcon;
	public Image Portrait;
	public Image PrimaryHealingIcon;
	public Image SecondaryHealingIcon;
	public Image GrenadeIcon;
	public Slider Healthbar;
	public Text PlayerName;

	// Use this for initialization
	void Start () {
		Portrait.sprite = Player.PlayerPortrait;
		Healthbar.minValue = 0;
		Healthbar.maxValue = Player.MAX_HEALTH;
		PlayerName.text = Player.GetName();
		PrimaryHealingIcon.sprite = Resources.Load<Sprite>("Sprites/HUD/HUD_Icons_HealthPack") ?? DefaultIcon;
		SecondaryHealingIcon.sprite = Resources.Load<Sprite>("Sprites/HUD/HUD_Icons_Painkiller") ?? DefaultIcon;
		GrenadeIcon.sprite = Resources.Load<Sprite>("Sprites/HUD/HUD_Icons_Adrenaline") ?? DefaultIcon; ;
	}
	
	// Update is called once per frame
	void Update () {
		Healthbar.normalizedValue = Player.GetPlayerNormalizedHealth();
	}
}
