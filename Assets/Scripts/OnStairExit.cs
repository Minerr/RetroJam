using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStairExit : MonoBehaviour {
	private StairController _stairController;

	// Use this for initialization
	void Start () {
		_stairController = GetComponentInParent<StairController>();
	}
	
	private void OnTriggerExit2D(Collider2D other) {
		_stairController.Reset();
	}

}
