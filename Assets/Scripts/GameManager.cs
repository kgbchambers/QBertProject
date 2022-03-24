using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{

    //Game setup prefabs
    public GameObject playerPref;
    public GameObject levelBlockPref;

    //Stats for player
    private int lives = 0;
    private int curLevel = 0;
    private int score = 0;
    private int highScore = 0;
    private int levelBlockColors;

    private Vector3 spawnPoint = new Vector3(0f, 1f, 0f);

    //array of integers that corresponds to a spaces color level (ie: 0 = not touched by Qbert, 1 = first color shift, etc)
    private Dictionary<Vector3, int> blockValues;

    //UI Text Objects to update
    public Text scoreText;
    public Text levelText;
    public Text livesText;
    public Text highScoreText;

    public void Start()
    {
        BuildLevel();
        ResetGame();
        UpdateUI();
    }


    private void UpdateUI()
    {
        scoreText.text = score + "";
        livesText.text = "Lives: " + lives;
        levelText.text = "Level: " + curLevel;
        if(score > highScore)
        {
            highScore = score;
            highScoreText.text = "Highest Score: " + highScore;
        }
    }

    private void BuildLevel()
    {
        Instantiate(playerPref, spawnPoint, Quaternion.identity);

        for (int y = 0; y > -7; y--)
        {
            for (int z = y; z <= 0; z++)
            {
                float x = Mathf.Abs(y) - Mathf.Abs(z);
                Instantiate(levelBlockPref, new Vector3(x, y, z), Quaternion.identity);
            }
        }
    }


    private void ResetLevel()
    {
        //Time.timeScale = 0f;
        playerPref.transform.position = spawnPoint;
        EnemyManager.Instance.WipeEnemies();
    }

    private void ResetGame()
    {
        lives = 3;
        curLevel = 1;
        score = 0;
    }


    public void LoseLife()
    {
        lives--;
        UpdateUI();
        ResetLevel();
    }

}



//Lerp (Time from point a to point b)
