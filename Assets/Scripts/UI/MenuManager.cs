using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private bool gameStarted = false;
    public GameObject gameStartScreen;
    public GameObject gameOverScreen;

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            gameStartScreen.SetActive(false);
            StartGame();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        SceneManager.LoadScene("Game");  
    }
}
