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
    public PlayerControl playerControl;

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
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    void Update()
    {
        float playerSpeed = playerControl.speed;
        if (Input.GetKeyDown(KeyCode.R) && playerSpeed == 0)
            {
                SceneManager.LoadScene("Game"); 
            }
    }

    public int GetScore()
    {
        return score;
    }
}
