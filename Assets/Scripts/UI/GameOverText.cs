using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverText : MonoBehaviour
{
    public TextMeshProUGUI  overText; 
    public GameManager gameManager;
    
    void Start()
    {
        DisplayScore();
    }

    void DisplayScore()
    {
        int finalScore = gameManager.GetScore();
        overText.text = "GAME OVER \n You got " + finalScore.ToString() + " coins \n Press R to restart.";
    }
}
