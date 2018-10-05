using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BulletBehavior : MonoBehaviour
{
    private float speed = 1f;
    private float damage = 10f;

    private Rigidbody2D body;
    private BoxCollider2D collider;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        body.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D()
    {
        Debug.Log("Bullet Hit Something");
    }
}