using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainableItemController : MonoBehaviour {

	private GameObject _mainCanvas;
	private GameObject _playerObject;
	private bool _displayOverlay;

	// Use this for initialization
	void Start () {
		_playerObject = GameObject.FindGameObjectWithTag("Player");
		_mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void OnGUI()
	{
		if(_displayOverlay)
		{

		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision == _playerObject.GetComponent<Collision2D>())
		{
			_displayOverlay = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if(collision == _playerObject.GetComponent<Collision2D>())
		{
			_displayOverlay = false;
		}
	}
}
