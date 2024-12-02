using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : Enemy
{
    protected float DELTA_X = 0.5f;

    private SpriteRenderer sprite;
    new void Awake()
    {
        base.Awake();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Bird to player vector
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x > DELTA_X)
        {
            isMovingRight = true;
            animator.SetInteger("DirX", 1);
        }
        else if (direction.x < -DELTA_X)
        {
            isMovingRight = false;
            animator.SetInteger("DirX", -1);
        }
        else
        {
            isMovingRight = false;
            animator.SetInteger("DirX", 0);
        }
       

        // TOFIX ANIMACION ARRIBA O ABAJO
        //  animator.SetInteger("DirY", direction.y != 0 ? 1 : 0);
        // New movement of bird
        transform.position += speed * Time.deltaTime * direction.normalized;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.OnPlayerKilled();
        }
    }
    void LateUpdate()
    {
        sprite.flipX = isMovingRight;
    }
}
