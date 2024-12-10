using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerControllerInputSystem : MonoBehaviour
{

    // Start is called before the first frame update{

    private SpriteRenderer sprite;
    //Debug
    [SerializeField] private Vector2 movement;

    [SerializeField] private float speed = 7f;
    [SerializeField] private float verticalSpeed = 6f;
    public delegate void OnPlayerKilled();
    public event OnPlayerKilled OnPlayerKilledEvent;

    private Animator animator;
    // [SerializeField] private bool isGrounded;

    private enum JumpState
    {
        Grounded,
        JumpingUp,
        Falling,
        Planning
    }
    [SerializeField] private JumpState jumpState = JumpState.Grounded;
    private Rigidbody2D rb;
    [SerializeField] private Collider2D collider;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }
    public void onPlayerKilled()
    {
        OnPlayerKilledEvent?.Invoke();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        var playerActionMap = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        var moveAction = playerActionMap.FindAction("Move");
        moveAction.started += OnMove;
        moveAction.canceled += OnMove;

        var jumpAction = playerActionMap.FindAction("Jump");
        jumpAction.started += OnJumpStarted;
        jumpAction.canceled += OnJumpCanceled;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        if (jumpState == JumpState.JumpingUp)
        {
            jumpState = JumpState.Falling;
        }
        if (jumpState == JumpState.Planning)
        {
            jumpState = JumpState.Falling;
        }
        // rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
    }


    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (jumpState == JumpState.Grounded)
        {
            jumpState = JumpState.JumpingUp;

        }
        if (jumpState == JumpState.Falling)
        {
            jumpState = JumpState.Planning;
        }


    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            movement = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            movement = Vector2.zero;
        }

    }

    void FixedUpdate()
    {
        if (jumpState == JumpState.Grounded)
        {
            if (movement.y > 0)
            {
                jumpState = JumpState.JumpingUp;
            }
            else if (rb.velocity.y < 0)
            {
                jumpState = JumpState.Falling;
            }

        }
        RaycastHit2D hit =
      Physics2D.Raycast(collider.bounds.center, Vector2.down);
        if (hit.collider)
        {
            if (jumpState != JumpState.JumpingUp)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    jumpState = JumpState.Grounded;
                }
            }
        }
        if (jumpState == JumpState.JumpingUp)
        {
            //apply a force to the player
            rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
        }
        if (jumpState == JumpState.Planning)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.75f);
        }

        //move the player
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Ground"))
        {

            jumpState = JumpState.Grounded;
        }
        if (col.gameObject.CompareTag("Roof"))
        {
            jumpState = JumpState.Falling;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(collider.bounds.center, collider.bounds.center + Vector3.down * (collider.bounds.extents.y + 0.1f));
    }

    // For player killed
   
    void Update()
    {
        updateAnimation();
    }

    void updateAnimation()
    {
        if (jumpState == JumpState.Grounded)
        {
            animator.SetInteger("DirXPlan", 0);
            animator.SetBool("PlayerJumpingRight", false);
            animator.SetBool("PlayerJumpingLeft", false);
            animator.SetBool("isGrounded", true);
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isPlanning", false);
            if (rb.velocity.x == 0)
            {
                animator.SetInteger("DirX", 0);

            }
            else if (rb.velocity.x > 0)
            {
                animator.SetInteger("DirX", 1);
                sprite.flipX = false;
            }
            else if (rb.velocity.x < 0)
            {
                animator.SetInteger("DirX", -1);
                sprite.flipX = true;
            }
        }

        if (jumpState == JumpState.Planning)
        {
            animator.SetBool("PlayerJumpingRight", false);
            animator.SetBool("PlayerJumpingLeft", false);
            animator.SetInteger("DirX", 0);
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isPlanning", true);
            if (rb.velocity.x == 0)
            {
                animator.SetInteger("DirXPlan", 0);
            }
            else if (rb.velocity.x > 0)
            {
                animator.SetInteger("DirXPlan", 1);
                sprite.flipX = false;
            }
            else if (rb.velocity.x < 0)
            {
                animator.SetInteger("DirXPlan", -1);
                sprite.flipX = true;
            }
        }
        if (jumpState == JumpState.JumpingUp)
        {
            animator.SetInteger("DirX", 0);
            animator.SetBool("isPlanning", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", true);
            if (rb.velocity.x > 0)
            {
                animator.SetBool("PlayerJumpingRight", true);

                animator.SetBool("PlayerJumpingLeft", false);
            }
            else if (rb.velocity.x < 0)
            {
                animator.SetBool("PlayerJumpingLeft", true);
                animator.SetBool("PlayerJumpingRight", false);

            }
            else if (rb.velocity.x == 0)
            {
                animator.SetBool("PlayerJumpingRight", false);
                animator.SetBool("PlayerJumpingLeft", false);
            }
        }
        if (jumpState == JumpState.Falling)
        {
            animator.SetBool("PlayerJumpingRight", false);
            animator.SetBool("PlayerJumpingLeft", false);
            animator.SetInteger("DirX", 0);
            animator.SetBool("isPlanning", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }

    }
    // TODO: jumping l and r , mantener salto que pueda planear cuando choque con el techo.
}
