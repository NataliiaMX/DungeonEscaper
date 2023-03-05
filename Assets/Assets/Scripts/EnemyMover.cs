using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float mooveSpeed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange;
    [SerializeField] private Animator animator;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private float distanceToTarget = Mathf.Infinity;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        MoveEnemy();
        FlipSprite();
        AnimateEnemy();
    }

    private void MoveEnemy()
    {
        distanceToTarget = Vector2.Distance(target.position, transform.position);

        if (target)
        {
            if (distanceToTarget <= chaseRange)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                moveDirection = direction;
                rb.velocity = new Vector2(moveDirection.x, 0f) * mooveSpeed;
            }
            else if (distanceToTarget > chaseRange)
            {
                rb.velocity = new Vector2 (0f,0f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void FlipSprite()
    {
        bool hasSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (hasSpeed)
        {
            if (rb.velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void AnimateEnemy()
    {
        bool hasSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Run", hasSpeed);
    }

    public void DestroyEnemy()
    {
        if(gameObject)
        {
           Destroy(gameObject); 
        }
    }
}
