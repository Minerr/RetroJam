using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealingType
{
	Primary,
	Secondary
}

[CreateAssetMenu(menuName = "Obtainable Objects/HealingItem")]
public class HealingItem : Obtainable
{
	[Range(0,100)]
	public float HealingPercentage;

	public HealingType HealingType;
}
