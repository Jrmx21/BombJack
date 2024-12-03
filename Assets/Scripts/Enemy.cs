using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    protected Animator animator;

    [SerializeField] protected float speed = 1.0f;

    // Reference to player
    [SerializeField] protected PlayerControllerInputSystem player;

    protected bool isMovingRight = true;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }
    protected void Start()
    {
        player= GameManager.Instance.Player;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
      
        }
    }
}
