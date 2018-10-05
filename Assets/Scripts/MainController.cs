using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public PlayerStats playerOne;
	public GameObject playerTwo;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			playerOne.currentHealth -= 10;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			playerOne.currentHealth += 10;
		}

		if(Input.GetKeyDown(KeyCode.Q))
		{

		}
		if(Input.GetKeyDown(KeyCode.W))
		{

		}
	}
}
