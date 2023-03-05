using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitPickup : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private int pointsToAdd;
    private Animator animator;
    private GameSessionController gameSession;
    private float soundVolume = 0.7f;
    private bool wasCollected = false;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        gameSession = FindObjectOfType<GameSessionController>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.transform.tag == "Player" && !wasCollected)
        {
           StartCoroutine(PickUpFruit()); 
        }
    }

    IEnumerator PickUpFruit()
    {
        wasCollected = true;
        animator.SetBool("Picked", true);
        gameSession.IncreaseScore(pointsToAdd);
        AudioSource.PlayClipAtPoint(audioClip, transform.position, soundVolume);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
