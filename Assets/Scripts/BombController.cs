using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombController : MonoBehaviour
{
    // Animation
    [SerializeField] private Sprite activeFrame01;
    [SerializeField] private Sprite activeFrame02;
    [SerializeField] private Sprite inactiveFrame01;

    [SerializeField] private Sprite inactiveCatchedFrame01;
    [SerializeField] private Sprite inactiveCatchedFrame02;
    [SerializeField] private Sprite inactiveCatchedFrame03;

    [SerializeField] private Sprite activeCatchedFrame01;

    //States 
    [SerializeField] private bool isActive = false;
    private bool isFinishing;
    // "Anim" active Time
    [SerializeField] private float activateTick = 0.1f;
    [SerializeField] private float catchedTick = 0.5f;
    private float timerAnim = 0f;
    // References to components
    private Collider2D collider2d;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (isActive)
        {
            spriteRenderer.sprite = activeFrame01;
            //empieza animación 
            timerAnim = activateTick;
        }
        else
        {
            spriteRenderer.sprite = inactiveFrame01;
        }
    }
    void Update()
    {
        if (isFinishing)
        {
            timerAnim -= Time.deltaTime;
            if (timerAnim > catchedTick / 4 * 3)
            {
                spriteRenderer.sprite = inactiveCatchedFrame01;
            }
            else if (timerAnim > catchedTick / 4 * 2)
            {
                spriteRenderer.sprite = inactiveCatchedFrame02;
            }
            else if (timerAnim > catchedTick / 4)
            {
                spriteRenderer.sprite = inactiveCatchedFrame03;
            }
            else
            {
                if (isActive)
                {
                    spriteRenderer.sprite = activeCatchedFrame01;
                }
            }

            if (timerAnim <= 0.0f)
            {
                // Destroy the bomb object
                Destroy(gameObject);
            }

            // If is finishing, return (ignore the rest of the update)
            return;
        }
        if (isActive)
        {
            timerAnim -= Time.deltaTime;
            if (timerAnim <= 0)
            {
                if (spriteRenderer.sprite == activeFrame01)
                {
                    spriteRenderer.sprite = activeFrame02;
                }
                else
                {
                    spriteRenderer.sprite = activeFrame01;
                }
                timerAnim = activateTick;
            }
        }
        else
        {
            spriteRenderer.sprite = inactiveFrame01;
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //inactive collider
            collider2d.enabled = false;
            //destroy the bomb
            isFinishing=true;
            //reset the timer
            catchedTick=0;
            Debug.Log("coge moneda");

        }
    }
}

// TODO: CHOCA EN EL TECHO Y SALTA INFINITO, LA ANIMACION DE CUANDO SE COGE LA BOMBA NO FUNCIONA BIEN
