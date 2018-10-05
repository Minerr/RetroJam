using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BulletBehavior : MonoBehaviour
{
    public float Speed = 1f;
    public float Damage = 10f;
    public float lifeTime = 1f;

    private Rigidbody2D body;
    private BoxCollider2D collider;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        body.velocity = transform.right * Speed;
        Destroy(this,lifeTime);
    }

    void OnTriggerEnter2D()
    {
        Debug.Log("Bullet Hit Something");
    }
}