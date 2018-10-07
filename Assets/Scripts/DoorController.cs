using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	[SerializeField] private Sprite ClosedSprite;
	[SerializeField] private Sprite OpenSprite;
	[SerializeField] private SpriteRenderer SpriteRenderer;
	[SerializeField] private MeshRenderer MeshRenderer;
	[SerializeField] private Transform _player;
	[SerializeField] private float _distToOpen;
	[SerializeField] private BoxCollider2D _collider;
	private bool _doorIsOpen = false;

	public GameObject HUD_PromptOpenDoor;
	public GameObject HUD_PromptCloseDoor;

	private bool _isPromptActive = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool canOpenDoor = ((_player.position - transform.position).magnitude <= _distToOpen);

		if(canOpenDoor)
		{
			_isPromptActive = true;

			if(_doorIsOpen)
			{
				HUD_PromptCloseDoor.SetActive(true);
				HUD_PromptOpenDoor.SetActive(false);
			}
			else
			{
				HUD_PromptOpenDoor.SetActive(true);
				HUD_PromptCloseDoor.SetActive(false);
			}


			if(Input.GetButtonDown("Interact"))
			{
				ToggleDoor();
			}
		}
		else if(_isPromptActive)
		{
			HUD_PromptCloseDoor.SetActive(false);
			HUD_PromptOpenDoor.SetActive(false);
		}
	}

	private void ToggleDoor() {
		if (_doorIsOpen) {
			CloseDoor();
		}
		else {
			OpenDoor();
		}
	}

	void CloseDoor() {
		_doorIsOpen = false;
		MeshRenderer.enabled = true;
		_collider.enabled = true;
		SpriteRenderer.sprite = ClosedSprite;
	}

	void OpenDoor() {
		_doorIsOpen = true;
		MeshRenderer.enabled = false;
		_collider.enabled = false;
		SpriteRenderer.sprite = OpenSprite;
	}
}
