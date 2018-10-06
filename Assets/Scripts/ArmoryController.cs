using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryController : MonoBehaviour {

	public Weapon[] Weapons;
	public HealingItem[] HealingItems;
	public Grenade[] Grenades;

	private List<Obtainable> AllItems;

	// Use this for initialization
	void Awake () {
		AllItems = new List<Obtainable>();
		AllItems.AddRange(Weapons);
		AllItems.AddRange(HealingItems);
		AllItems.AddRange(Grenades);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public T GetItem<T>(string itemName) where T : Obtainable
	{
		foreach(var item in AllItems)
		{
			if(item.ItemName == itemName)
			{
				return item as T;
			}
		}

		return null;
	}
}
