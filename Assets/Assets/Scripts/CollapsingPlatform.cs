using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        StartCoroutine(CollapsePlatform());
        
    }

    IEnumerator CollapsePlatform()
    {
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
    }
}
