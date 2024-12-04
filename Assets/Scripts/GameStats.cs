using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//using UnityEditor.Timeline.TimelinePlaybackControls;


public class GameStats : MonoBehaviour
{
    public static int numCoinsCollected = 0;
    public static int numDiamondsCollected = 0;
    public static int numEnemiesKilled = 0;
    public static int gameScore = 0;

    public TextMeshProUGUI coinCountText;
    public TextMeshProUGUI countDiamondText;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI gameScoreText;

    // Start is called before the first frame update
    void Start()
    {
        coinCountText.text = "Coins Collected = " + numCoinsCollected.ToString();
        countDiamondText.text = "Diamonds Collected = " + numDiamondsCollected.ToString();
        enemyCountText.text = "Enemies Killed = " + numEnemiesKilled.ToString();
        gameScoreText.text = "Game Score = " + gameScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            numCoinsCollected += 1;
            gameScore += 5;
        }
        else if (other.gameObject.CompareTag("Diamond"))
        {
            numDiamondsCollected += 1;
            gameScore += 10;
        }
    }

    private void OnGUI()
    {
        coinCountText.text = "Coins Collected = " + numCoinsCollected.ToString();
        countDiamondText.text = "Diamonds Collected = " + numDiamondsCollected.ToString();
        enemyCountText.text = "Enemies Killed = " + numEnemiesKilled.ToString();
        gameScoreText.text = "Game Score = " + gameScore.ToString();

        if (gameScore > 100)
        {
            EndGame();
        }
    }

    public static void UpdateEnemyiesKilled()
    {
        numEnemiesKilled += 1;
        gameScore += 15;
    }

    void EndGame()
    {
        SceneManager.LoadScene("GameEndScene");
    }

}
