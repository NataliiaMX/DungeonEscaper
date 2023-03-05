using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSessionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI plusScore;
    private ScenePersist scenePersist;
    private int score = 0;
    private int playerLives = 3;
    private void Awake() 
    {
        int gameSessionsNumber = FindObjectsOfType<GameSessionController>().Length;

        if(gameSessionsNumber > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        DisplayPlayerLives();
    }

    public void HandlePlayersDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        scenePersist = FindObjectOfType<ScenePersist>();
        scenePersist.ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerLives--;
        DisplayPlayerLives();
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void DisplayPlayerLives()
    {
        livesText.text = playerLives.ToString();
    }

    public void IncreaseScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
        StartCoroutine(IncreaseScoreText(pointsToAdd));
    }

    IEnumerator IncreaseScoreText(int pointsToAdd)
    {
        plusScore.text = "+" + pointsToAdd.ToString();
        yield return new WaitForSeconds(1.5f);
        plusScore.text = "";
    }
}
