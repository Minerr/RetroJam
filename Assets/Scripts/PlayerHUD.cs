using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

	public PlayerController player;
	public Image image;
	public Slider healthbar;

	// Use this for initialization
	void Start () {
		image.sprite = player.PlayerPortrait;
		healthbar.minValue = 0;
		healthbar.maxValue = player.MAX_HEALTH;
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.normalizedValue = player.GetPlayerNormalizedHealth();
	}
}
