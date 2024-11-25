using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{

    //Debug
    [SerializeField] private Vector2 movement;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private bool isGrounded;

    private Rigidbody2D rb;
    [SerializeField] private Collider2D collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement.x = 1;
        }
        else
        {
            movement.x = 0;
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            //apply a force to the player
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

        //move the player
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Debug.Log("suelo");
            isGrounded = true;
        }
    }
}
