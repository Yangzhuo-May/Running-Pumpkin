using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverText : MonoBehaviour
{
    public TextMeshProUGUI  overText; 
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        DisplayScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayScore()
    {
        int finalScore = gameManager.GetScore();
        overText.text = "GAME OVER \n You got " + finalScore.ToString() + " coins \n Press R to restart.";
    }
}
