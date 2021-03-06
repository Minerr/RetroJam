﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private PlayerController _playerController;

    private Animator _animator;

    [SerializeField] private float Speed = 3f;

    [SerializeField] private float JumpSpeed = 3f;

    private bool isOnGround = true;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerController.IsDead)
        {
            return;
        }
        var horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontal) > 0.1)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
        _rigidbody.velocity = new Vector2(horizontal * Speed, _rigidbody.velocity.y);


        var t1 = Physics2D.Raycast(_rigidbody.position - new Vector2(0.45f, 1.51f), new Vector2(0, -1), 0.01f);
        var t2 = Physics2D.Raycast(_rigidbody.position - new Vector2(-0.45f, 1.51f), new Vector2(0, -1), 0.01f);

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

    void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpSpeed);
    }
}