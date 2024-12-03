using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MummyController : Enemy
{

    // Left and right max movement
    [SerializeField] private float leftMaxX = -4.5f;
    [SerializeField] private float rightMaxX = 2f;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    new void Start()
    {
        base.Start();
        animator.Play("Idle");
        // position the mummy at the left point
        transform.position = new Vector2(leftMaxX, transform.position.y);
        isMovingRight = true;
        animator.Play("MummyWalkRight");
    }

    void Update()
    {
        if (isMovingRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x >= rightMaxX)
            {
                isMovingRight = false;
                animator.Play("MummyWalkLeft");
            }
        }
        else
        {

            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x <= leftMaxX)
            {
                isMovingRight = true;
                animator.Play("MummyWalkRight");
            }

        }

    }
}
