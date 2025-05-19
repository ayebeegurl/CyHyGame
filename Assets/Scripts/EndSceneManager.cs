using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public Text endTitleText;
    public Text scoreText;
    public Button restartButton;
    public Button mainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        int score = GameManager.Instance.score;
        int wrongAnswers = GameManager.Instance.wrongAnswers;
        int maxScore = GameManager.Instance.maxScore;

        if (wrongAnswers >= 2)
        {
            endTitleText.text = "Game Over!";
        }
        else
        {
            endTitleText.text = "You Win!!";
        }

        scoreText.text = $"Jawaban benar: {score}/{maxScore}";

        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartGame()
    {
        
        SceneManager.LoadScene("level 1"); 
    }

    public void GoToMainMenu()
    {
        
        SceneManager.LoadScene("Menu");
    }
}
