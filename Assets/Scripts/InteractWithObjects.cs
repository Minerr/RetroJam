using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObjects : MonoBehaviour
{
	private bool _showPickUpLine;
	private Collider2D _colliderItem;
	private PlayerController _controller;

	public GameObject HUD_PromptPickUp;

	// Use this for initialization
	void Start()
	{
		_showPickUpLine = false;
		_controller = GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		if(_colliderItem != null)
		{
			if(Input.GetButtonDown("Interact"))
			{
				Obtainable item = _colliderItem.GetComponent<Item>().ItemType;
				_controller.EquipItem(item);
				Destroy(_colliderItem.gameObject);
				_colliderItem = null;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Item")
		{
			_showPickUpLine = true;
			_colliderItem = collider;
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Item")
		{
			_showPickUpLine = false;
			_colliderItem = null;
		}
	}

	private void OnGUI()
	{
		if(_showPickUpLine)
		{
			HUD_PromptPickUp.SetActive(true);
		}
		else
		{
			HUD_PromptPickUp.SetActive(false);
		}
	}

	private void DrawPickUpLine()
	{
		if(_showPickUpLine)
		{
			Texture tex_pressToPickUp = Resources.Load<Texture>("Sprites/HUD/Text_PressToPickUp");

			float imageScale = 3;
			float imageWidth = tex_pressToPickUp.width * imageScale;
			float imageHeight = tex_pressToPickUp.height * imageScale;
			float imageCenterX = imageWidth / 2;
			float imageCenterY = 200 + (imageHeight / 2);

			Vector2 playerInScreenPos = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

			GUI.DrawTexture(new Rect(playerInScreenPos.x - imageCenterX, playerInScreenPos.y - imageCenterY, imageWidth, imageHeight), tex_pressToPickUp, ScaleMode.StretchToFill, true, 10.0F);
		}
	}

}
