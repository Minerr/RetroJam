using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ZombieMovement : MonoBehaviour
{

    private Rigidbody2D _rigidbody;

    [SerializeField] private float Speed = 3f;

    private bool canMove = true;


    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    public void MoveToPlayer(GameObject target)
    {
        if (canMove)
        {
            var horizontal = Mathf.Sign(target.transform.position.x - transform.position.x);
            _rigidbody.velocity = new Vector2(horizontal * Speed, _rigidbody.velocity.y);
        }
    }

    public void StopMoving()
    {
        canMove = false;
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        Invoke("WakeBody", 0.1f);

    }

    public void WakeBody()
    {
        canMove = true;
    }

}
