using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI coinCountText;
    public TextMeshProUGUI diamondCountText;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI gameScoreText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGameButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;       
        #else
            Application.Quit();
        #endif
    }

    public void StartGameButton() 
    { 
        SceneManager.LoadScene("CoinGame"); 
    }

    public void RestartGame()
    {
        GameStats.numCoinsCollected = 0;
        GameStats.numDiamondsCollected = 0;
        GameStats.numEnemiesKilled = 0;
        GameStats.gameScore = 0;
        SceneManager.LoadScene("CoinGame");
    }

    private void OnGUI()
    {
        if (coinCountText)
        {
            coinCountText.text = "Coins Collected = " + GameStats.numCoinsCollected.ToString();
        }
        if (diamondCountText)
        {
            diamondCountText.text = "Diamonds Collected = " + GameStats.numDiamondsCollected.ToString();
        }
        if (enemyCountText)
        {
            enemyCountText.text = "Enemies Killed = " + GameStats.numEnemiesKilled.ToString();
        }
        if (gameScoreText)
        {
            gameScoreText.text = "Game Score = " + GameStats.gameScore.ToString();
        }
    }
}
