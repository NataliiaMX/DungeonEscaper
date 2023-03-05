using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    [SerializeField] private Sprite jumpSprite;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private DamageHandler isAlive;
    private CapsuleCollider2D playerCollider;
    private Vector2 moveInput;
    private Vector2 runVelocity;
    private Vector2 climbVelocity;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        MovePlayer();
        ClimbLadder();
        FlipSprite();
        AnimatePlayer();
    }

    private void ClimbLadder()
    {
        if (isAlive.GetIsAlive() == false) {return;}
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
            rb.velocity = climbVelocity;
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 1.2f;
        }
    }

    private void MovePlayer()
    {
        if (isAlive.GetIsAlive() == false) {return;}
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            GetComponent<SpriteRenderer>().sprite = jumpSprite;
        }
        runVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = runVelocity;

        animator.SetBool("IsRunning", true);
    }

    private void FlipSprite()
    {
        bool hasSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (hasSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    private void AnimatePlayer()
    {
        if (isAlive.GetIsAlive() == false) {return;}
        bool hasSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("IsRunning", hasSpeed);
    }

    private void OnMove(InputValue value)
    {
        if (isAlive.GetIsAlive() == false) {return;}
        GetComponent<Animator>().enabled = true;
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (isAlive.GetIsAlive() == false) {return;}
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || playerCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            if (value.isPressed)
            {
                rb.velocity += new Vector2(0f, jumpHeight);
                GetComponent<Animator>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = jumpSprite;
            }
        }
    }
}
