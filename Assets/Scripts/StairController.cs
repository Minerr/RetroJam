using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairController : MonoBehaviour {
	[SerializeField] private PolygonCollider2D RidgidStair;


	public void Reset() {
		RidgidStair.enabled = false;
	}

	public void Show() {
		RidgidStair.enabled = true;
	}
}
