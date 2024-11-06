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
    //public Text scoreTest;
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
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

    }

    // Update is called once per frame
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

    void TaskOnClick()
    {
		
	}
}
