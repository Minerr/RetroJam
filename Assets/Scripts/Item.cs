﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour {

	public Obtainable ItemType;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sprite = ItemType.Sprite;
		GetComponent<BoxCollider2D>().isTrigger = true;
		this.tag = "Item";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
