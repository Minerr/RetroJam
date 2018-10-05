using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	    var Hori = Input.GetAxisRaw("AimHorizontal");
	    var Vert = Input.GetAxisRaw("AimVertical");

	    Debug.Log(string.Format("H = {0} : V = {1}", Hori, Vert));

	    Vector2 dir = new Vector2(Hori, Vert);

        Debug.DrawLine(transform.position,transform.position + new Vector3(dir.x,dir.y,0)*5);


	}
}
