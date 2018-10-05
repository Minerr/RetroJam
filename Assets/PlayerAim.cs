using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private float ControllerThreshHold = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	    var Hori = Input.GetAxisRaw("AimHorizontal");
	    var Vert = Input.GetAxisRaw("AimVertical");


	    Vector2 dir = new Vector2(Vert, Hori);

	    if (dir.magnitude > ControllerThreshHold)
	    {
	        var dirNormal = dir.normalized;

	        var angle = Mathf.Atan2(dirNormal.x, dirNormal.y) * Mathf.Rad2Deg;
	        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	        transform.rotation = rotation;
	    }


    }
}
