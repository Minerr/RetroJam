using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Obtainable Objects/Grenade")]
public class Grenade : Obtainable
{
	[Range(0,1000)]
	public float Radius;
	[Range(0,10000)]
	public float BaseDamage;
}

