using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Obtainable Objects/SecondaryHealing")]
public class SecondaryHealing : Obtainable
{
	[Range(0, 100)]
	public float HealingPercentage;
}
