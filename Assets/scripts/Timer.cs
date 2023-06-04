using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float totalTime = 40f; // Total time in seconds
    private float currentTime;
    private bool isGameOver = false;
    public Text timerText;
    public GameObject gameOverScreen;

    private void Start()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isGameOver = true;
                GameOver();
            }

            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Floor(currentTime).ToString();
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);
        // You can add additional game over logic here, such as stopping the car or showing score, etc.
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
