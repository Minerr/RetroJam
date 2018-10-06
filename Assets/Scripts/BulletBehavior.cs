using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BulletBehavior : MonoBehaviour
{
    public float Speed = 0f;
    public float Damage = 0f;
    public float lifeTime = 0f;

    private Rigidbody2D body;
    public SpriteRenderer SpriteRenderer;

    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        body.velocity = transform.right * Speed;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Zombie"))
        {
            col.gameObject.GetComponent<ZombieController>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}