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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            gameStartScreen.SetActive(false);
            // Start_Image.enabled = false;
            StartGame();
        }
    }

    public void Setup()
    {
        
    }

    public void StartGame()
    {
        gameStarted = true;
        SceneManager.LoadScene("Game");  
    }

    
}
