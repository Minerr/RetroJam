using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _playerNumber;
    private float _currentHealth;

    public readonly float MAX_HEALTH = 100f;

    public Sprite PlayerPortrait;
    public Weapon EquippedWeapon;
    public Grenade EquippedGrenade;
    public PrimaryHealing EquippedPrimaryHealing;
    public SecondaryHealing EquippedSecondaryHealing;

    public int PlayerNumber
    {
        get { return _playerNumber; }
    }

    // Use this for initialization
    void Start()
    {
        _currentHealth = MAX_HEALTH;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetPlayerNormalizedHealth()
    {
        return _currentHealth / MAX_HEALTH;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        Debug.Log(string.Format("i took {0} damage, current health {1}", amount, _currentHealth));

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("I Am Dead");
    }

    public void GainHealth(float amount)
    {
        _currentHealth += amount;

        if (_currentHealth >= MAX_HEALTH)
        {
            _currentHealth = MAX_HEALTH;
        }
    }

    public string GetName()
    {
        return "Player0" + _playerNumber;
    }

}
