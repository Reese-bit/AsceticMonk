using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;

public class passPlayer : Character
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed;
    [SerializeField] private int extraJump;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;
    private bool isGround;
    
    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        playerInput.onMove += Move;
        playerInput.onStopMove += StopMove;
        playerInput.onAttack += Attack;
        playerInput.onJump += Jump;
    }

    private void OnDisable()
    {
        playerInput.onMove -= Move;
        playerInput.onStopMove -= StopMove;
        playerInput.onAttack -= Attack;
        playerInput.onJump -= Jump;
    }

    private void Start()
    {
        playerInput.EnableGamePlayInput();
    }

    void Move(Vector2 moveDirection)
    {
        rb.velocity = new Vector2(moveDirection.x　* speed * Time.deltaTime,rb.velocity.y);
        anim.SetInteger("AnimState",2);
        if (moveDirection != Vector2.zero)
        {
            transform.localScale = new Vector3(moveDirection.x, 1, 1);
        }
    }

    void StopMove()
    {
        rb.velocity = Vector2.zero;
        anim.SetInteger("AnimState",0);
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void Jump()
    {
        if (isGround)
        {
            extraJump = 1;
        }
        if (extraJump > 0)
        {
            anim.SetTrigger("Jump");
            isGround = false;
            anim.SetBool("Grounded", isGround);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJump--;
        }
    }
}
