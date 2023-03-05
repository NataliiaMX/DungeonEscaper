using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    private GameSessionController gameSessionController;
    private Animator animator;
    private bool isAlive = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameSessionController = FindObjectOfType<GameSessionController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Hazard" || other.transform.tag == "Enemy")
        {
            StartCoroutine(DieGradually());
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.transform.tag == "Hazard" || other.transform.tag == "Enemy")
        {
            StopAllCoroutines();
        }
    }

    private void DieInstantly()
    {
        animator.SetBool("GotHit", true);
        isAlive = false;
        Invoke("ToggleAliveStatus", 1f);
        gameSessionController.HandlePlayersDeath();
    }

    IEnumerator DieGradually()
    {
        for (int i = 0; i <= playerHealth; i++)
        {
            if (playerHealth > 0)
            {
                playerHealth--;
                yield return new WaitForSeconds(1f);
            } 
            else
            {
                animator.SetBool("GotHit", true);
                DieInstantly();
            }
        }
    }

    private void ToggleAliveStatus()
    {
        gameObject.SetActive(false);
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }
}
