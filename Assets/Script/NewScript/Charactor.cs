using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Charactor : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider cupsuleCollider;

    public float jumpSpeed = 5;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cupsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Move(Vector2 input)
    {
    }

    void OnMove(InputValue inputValue)
    {
        Move(inputValue.Get<Vector2>());
    }

    public void Jump(bool state)
    {
        if (state)
        {
            rb.velocity += Vector3.up * jumpSpeed;
        }
    }

    void OnJump(InputValue inputValue)
    {
        Jump(inputValue.isPressed);
    }
}