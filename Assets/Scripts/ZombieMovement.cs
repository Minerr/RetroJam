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
    private GameObject _target;

    [SerializeField] private float Speed = 3f;
    [SerializeField] private float VisionRange = 10f;


    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        InvokeRepeating("FindPlayer", 0.0f, Random.Range(0.4f, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
    }

    void MoveToPlayer()
    {
        var horizontal = Mathf.Sign(_target.transform.position.x - transform.position.x);
        _rigidbody.velocity = new Vector2(horizontal * Speed, _rigidbody.velocity.y);
    }

    void StopMoving()
    {
        _rigidbody.velocity = new Vector2(0,_rigidbody.velocity.y);
    }

    void FindPlayer()
    {
        if (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("Player found");
        }
        else
        {
            int layerMask = 1 << 8;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _target.transform.position - transform.position,
                VisionRange,LayerMask.GetMask("Player")+LayerMask.GetMask("Default"));
            Debug.DrawRay(transform.position, _target.transform.position - transform.position);
            Debug.Log("Trying to hit player with ray");

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    _target = hit.collider.gameObject;
                    MoveToPlayer();
                    Debug.Log("Move to player");
                }
                else
                {
                    StopMoving();
                }
            }

        }
    }


}
