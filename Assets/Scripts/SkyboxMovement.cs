using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMovement : MonoBehaviour
{

    public Vector3 offset;
    public float xMul = 1f;
    public float yMul = 1f;

    void Update ()
	{
	    float CamX = Camera.main.transform.position.x*xMul;
	    var CamY = Camera.main.transform.position.y*yMul;

        var myPos = new Vector3(CamX,CamY,transform.position.z)+offset;

	    transform.position = myPos;
	}
}
