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

    [SerializeField] private GameManager gameManager;
    //States 
    [SerializeField] private bool isActive = false;
    private bool isFinishing;
    // "Anim" active Time
    [SerializeField] private float activateTick = 0.1f;
    [SerializeField] private float catchedTick = 0.5f;
    private float timerAnim = 0f;
    private float timerAnimFinish = 0f;
    // References to components
    private Collider2D collider2d;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();

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
        timerAnimFinish = catchedTick;
    }
    void Update()
    {
         if (GameManager.Instance.IsPaused)
        {
            return;
        }
        if (isFinishing)
        {

            timerAnimFinish -= Time.deltaTime;
            if (timerAnimFinish > catchedTick / 4 * 3 && !isActive)
            {
                spriteRenderer.sprite = inactiveCatchedFrame01;
            }
            else if (timerAnimFinish > catchedTick / 4 * 2 && !isActive)
            {
                spriteRenderer.sprite = inactiveCatchedFrame02;
            }
            else if (timerAnimFinish > catchedTick / 4 && !isActive)
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

            if (timerAnimFinish <= 0.0f)
            {
                // Destroy the bomb object
                Destroy(gameObject);
                timerAnimFinish = catchedTick;
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
            isFinishing = true;
            //reset the timer
            if (isActive)
            {
                gameManager.puntuar(200);
            }
            else
            {
                gameManager.puntuar(100);
            }

        }
    }
}

// TOFIX 1: Al pulsar "w" el jugador vuela infinito y lo mismo con "uparrow"
