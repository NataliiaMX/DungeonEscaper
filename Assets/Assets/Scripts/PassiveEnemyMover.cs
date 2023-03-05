using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEnemyMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() 
    {
        MoveThis();
    }

    private void MoveThis()
    {
        rb.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }

    private void FlipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1f);
    }

    public void DestroyEnemy()
    {
        if(gameObject)
        {
           Destroy(gameObject); 
        }
    }
}
