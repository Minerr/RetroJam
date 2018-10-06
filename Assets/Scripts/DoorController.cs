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
	private bool _doorIsOpen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Interact")) {
			if ((_player.position - transform.position).magnitude <= _distToOpen) {
				ToggleDoor();
			}
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
