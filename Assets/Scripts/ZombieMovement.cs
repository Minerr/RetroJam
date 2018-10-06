using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ZombieMovement : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private CircleCollider2D _vision;
    private GameObject _target;

    [SerializeField] private float Speed = 3f;
    [SerializeField] private float JumpSpeed = 3f;
    [SerializeField] private float VisionRange = 10f;

    private bool isOnGround = true;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _vision = GetComponent<CircleCollider2D>();
        _vision.radius = VisionRange;
    }

    // Update is called once per frame
    void Update()
    {

        if (_target != null)
        {
            MoveToPlayer();
        }

        var t1 = Physics2D.Raycast(_rigidbody.position - new Vector2(0.45f, 1.01f), new Vector2(0, -1), 0.01f);
        var t2 = Physics2D.Raycast(_rigidbody.position - new Vector2(-0.45f, 1.01f), new Vector2(0, -1), 0.01f);

        if (t1.transform != null || t2.transform != null)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            Jump();
        }
    }

    void MoveToPlayer()
    {
        var horizontal = _target.transform.position.x - transform.position.x;
        _rigidbody.velocity = new Vector2(horizontal * Speed, _rigidbody.velocity.y);
    }

    void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpSpeed);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            _target = collider.gameObject;
        }
    }
}
