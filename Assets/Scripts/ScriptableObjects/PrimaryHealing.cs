using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Obtainable Objects/PrimaryHealing")]
public class PrimaryHealing : Obtainable
{
	[Range(0,100)]
	public float HealingPercentage;
}
