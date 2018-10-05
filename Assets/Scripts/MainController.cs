using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public PlayerController playerOne;
	public PlayerController playerTwo;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			playerOne.TakeDamage(10);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			playerOne.GainHealth(25);
		}

		if(Input.GetKeyDown(KeyCode.Q))
		{

		}
		if(Input.GetKeyDown(KeyCode.W))
		{

		}
	}
}
