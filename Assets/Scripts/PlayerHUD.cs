using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

	public PlayerStats playerStats;
	public Image image;
	public Slider healthbar;

	// Use this for initialization
	void Start () {
		image.sprite = playerStats.playerImage;
		healthbar.minValue = 0;
		healthbar.maxValue = playerStats.maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.normalizedValue = playerStats.currentHealth / playerStats.maxHealth;
	}
}
