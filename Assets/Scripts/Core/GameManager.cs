using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    int score;
    public static GameManager inst;
    public Button GamePlayButton;
    public TextMeshProUGUI  scoreTest; 
    public PlayerController playerController;

    private const string GAME_SENCE = "Game";
    private const string PLAYER_TAG = "Player";

    public void IncrementScore()
    {
        score++;
        scoreTest.text = "SCORE: " + score;
    }

    private void Awake()
    {
        inst = this;
    }
    void Start()
    {
        playerController = GameObject.FindWithTag(PLAYER_TAG).GetComponent<PlayerController>();
    }

    void Update()
    {
        float playerSpeed = playerController.speed;
        if (Input.GetKeyDown(KeyCode.R) && playerSpeed == 0)
        {
            SceneManager.LoadScene(GAME_SENCE); 
        }
    }

    public int GetScore()
    {
        return score;
    }
}
