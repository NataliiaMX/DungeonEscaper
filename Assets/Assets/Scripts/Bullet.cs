using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject player;
    private EnemyMover activeEnemy;
    private PassiveEnemyMover passiveEnemy;
    private Rigidbody2D rb;
    private float bulletSpeed = 20f;
    private float playerLocalScale;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLocalScale = player.transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        activeEnemy = FindObjectOfType<EnemyMover>();
        passiveEnemy = FindObjectOfType<PassiveEnemyMover>();
    }
    private void Start()
    {
        GetVelocity();
        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        KillEnemy(other);
    }

    private void KillEnemy(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (activeEnemy)
            {
                activeEnemy.DestroyEnemy();
                passiveEnemy.DestroyEnemy();
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GetVelocity()
    {
        rb.velocity = new Vector2(bulletSpeed * playerLocalScale, 0f);
        // rb.AddForce(new Vector2(bulletSpeed * player.transform.localScale.x, 0f), ForceMode2D.Force);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
