using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private float levelLoadDelay = 1f;
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
       
       if (other.transform.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        } 
    }

    IEnumerator LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        if (currentSceneIndex == 3)
        {
            currentSceneIndex = 0;
        }
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

