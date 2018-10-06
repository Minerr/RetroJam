using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTop : MonoBehaviour {
	private StairController _stairController;

	// Use this for initialization
	void Start () {
		_stairController = GetComponentInParent<StairController>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		_stairController.Show();
	}
}
