using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public readonly float MAX_HEALTH = 100f;
	public Sprite PlayerPortrait;

	public int PlayerNumber { get { return _playerNumber; } }
	
	[SerializeField]
	private int _playerNumber;
	private float _currentHealth;
	private Weapon _equippedWeapon;
	private Grenade _equippedGrenade;

	private SpecialItem _equippedSpecialItem;
	private HealingItem _equippedItem;

	// Use this for initialization
	void Start () {
		_currentHealth = MAX_HEALTH;
	}
	
	// Update is called once per frame
	void Update () {
		
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
		}
	}

	public void GainHealth(float amount)
	{
		_currentHealth += amount;

		if(_currentHealth >= MAX_HEALTH)
		{
			_currentHealth = MAX_HEALTH;
		}
	}
}
