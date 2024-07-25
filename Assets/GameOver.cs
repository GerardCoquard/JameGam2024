using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public GameObject gameOverScreen;
    public float timeToGameOver;
    public TextMeshProUGUI roundsText;

    private void Awake()
    {
        instance = this;
    }

    public void Activate()
    {
        StartCoroutine(WaitFor());
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(timeToGameOver);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        roundsText.text = "You lasted for " + WaveManager.instance.GetCurrentWave() + " rounds...";

    }
}
