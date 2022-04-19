using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{

    //Game setup prefabs
    public GameObject playerPref;
    public GameObject levelBlockPref;
    public GameObject curLevelTarget;

    public GameObject saucerPref;

    //Stats for player
    private int lives = 0;
    private int curLevel = 0;
    private int score = 0;
    private int highScore = 0;


    private Vector3 spawnPoint = new Vector3(0f, 1f, 0f);
    private Vector3 saucerSpawn;
    private Vector3 temp;
    private string objectKey;
    private int scoreLivesCounter;
    private bool levelStart;

    //array of integers that corresponds to a spaces color level (ie: 0 = not touched by Qbert, 1 = first color shift, etc)
    private Dictionary<string, GameObject> blockValues = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> saucers = new Dictionary<string, GameObject>();

    private string key;

    //UI Text Objects to update
    public Text scoreText;
    public Text levelText;
    public Text livesText;
    public Text highScoreText;

    public void Start()
    {
        levelStart = true;
        BuildLevel();
        ResetGame();
        UpdateUI();
        EnemyManager.Instance.EnableSpawner(curLevel);
    }


    private void UpdateUI()
    {
        scoreText.text = score + "";
        livesText.text = "Lives: " + lives;
        levelText.text = "Level: " + curLevel;
        highScoreText.text = "Highest Score: " + highScore;
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "Highest Score: " + highScore;
        }
    }


    private void BuildLevel()
    {
        playerPref = Instantiate(playerPref, spawnPoint, Quaternion.identity);

        for (int y = 0; y > -7; y--)
        {
            for (int z = y; z <= 0; z++)
            {
                int x = Mathf.Abs(y) - Mathf.Abs(z);
                temp = new Vector3(x, y, z);
                GameObject newGO = Instantiate(levelBlockPref, temp, Quaternion.identity);
                objectKey = temp.ToString();
                //Debug.Log(objectKey);

                if (!blockValues.ContainsKey(objectKey))
                {
                    blockValues.Add(objectKey, newGO);
                }                    
                else
                    Debug.LogError("Duplicate block attempted to be added at location: " + temp);
            }
        }
        UpdateLevel();
    }
    

    public void ResetLevel()
    {
        //Time.timeScale = 0f;
        playerPref.transform.position = spawnPoint;
        
        PlayerController.Instance.ResetPlayer();
        EnemyManager.Instance.WipeEnemies();
    }


    private void ResetGame()
    {
        lives = 3;
        curLevel = 1;
        score = 0;
        scoreLivesCounter = 1;
        EnemyManager.Instance.currentLevel = curLevel;
        ResetBlockValues();
    }


    //check if block exist in location, and increment color value
    public void CheckBlockValue(string blockToCheck)
    {
        if (blockValues.ContainsKey(blockToCheck)){
            blockValues[blockToCheck].GetComponent<Cube>().PlayerCubeUpdate();
            CheckWinCondition();
            return;
        }
        else if(saucers.Count > 0)
        {
            if (saucers.ContainsKey(blockToCheck))
            {
                Destroy(saucers[blockToCheck].gameObject);
                ResetLevel();
                return;
            }
        }
        LoseLife();
    }



    public void CheckEnemyBlock(string blockToCheck)
    {
        if (blockValues.ContainsKey(blockToCheck))
        {
            blockValues[blockToCheck].GetComponent<Cube>().EnemyCubeUpdate();
        }
    }


    private void CheckWinCondition()
    {
        int winCheck = 0;
        foreach (GameObject cube in blockValues.Values)
        {
            int value = cube.GetComponent<Cube>().value;
            if(curLevel <= 3)
            {
                if(value == curLevel)
                {
                    winCheck++;
                }
            }
        }
        if(winCheck >= blockValues.Count)
        {
            UpdateLevel();
            ResetLevel();
        }
    }


    private void ResetBlockValues()
    {
        foreach (GameObject cube in blockValues.Values)
        {
            cube.GetComponent<Cube>().value = 0;
            cube.GetComponent<Cube>().curLevel = 0;
            cube.GetComponent<Cube>().NextLevel();
        }
    }


    public void AddPoints(int points)
    {
        score += points;
        if(score > highScore)
        {
            highScore = score;
        }
        if(score > (7500 * scoreLivesCounter))
        {
            scoreLivesCounter++;
            lives++;
        }
        UpdateUI();
    }


    public void UpdateLevel()
    {
        curLevel++;
        foreach (GameObject cube in blockValues.Values)
        {
            cube.GetComponent<Cube>().NextLevel();
        }
        foreach (GameObject saucer in saucers.Values)
        {
            saucer.GetComponent<Saucer>().curLevel = curLevel;
        }
        ResetLevel();
        curLevelTarget.GetComponent<Cube>().TargetCubeChange();
        EnemyManager.Instance.currentLevel++;
        if (!levelStart)
        {
            AddPoints(1000 + (250 * (curLevel - 1)));
            SpawnSaucer();
        }
        levelStart = false;
        UpdateUI();

    }


    private void SpawnSaucer()
    {
        int choice = Random.Range(0, 10);
        if (saucers.Count < 2)
        {
            Vector3 saucerSpawn = Vector3.zero;
            switch (choice)
            {
                case 0:
                    saucerSpawn = new Vector3(-1f, -1f, -2f);
                    key = saucerSpawn.ToString();
                    saucers.Add(key, Instantiate(saucerPref, saucerSpawn, Quaternion.identity));
                    break;
                case 1:
                    saucerSpawn = new Vector3(-1f, -2f, -3f);
                    key = saucerSpawn.ToString();
                    saucers.Add(key, Instantiate(saucerPref, saucerSpawn, Quaternion.identity));
                    break;
                case 2:
                    saucerSpawn = new Vector3(-1f, -3f, -4f);
                    key = saucerSpawn.ToString();
                    saucers.Add(key, Instantiate(saucerPref, saucerSpawn, Quaternion.identity));
                    break;
                case 3:
                    saucerSpawn = new Vector3(-1f, -4f, -5f);
                    key = saucerSpawn.ToString();
                    saucers.Add(key, Instantiate(saucerPref, saucerSpawn, Quaternion.identity));
                    break;
                case 4:
                    saucerSpawn = new Vector3(-1f, -5f, -6f);
                    key = saucerSpawn.ToString();
                    saucers.Add(key, Instantiate(saucerPref, saucerSpawn, Quaternion.identity));
                    break;
                case 5:
                    saucerSpawn = new Vector3(2f, -1f, 1f);
                    key = saucerSpawn.ToString();
                    saucers.Add(key, Instantiate(saucerPref, saucerSpawn, Quaternion.identity));
                    break;
                case 6:
                    saucerSpawn = new Vector3(3f, -2f, 1f);
                    key = saucerSpawn.ToString();
                    saucers.Add(key, Instantiate(saucerPref, saucerSpawn, Quaternion.identity));
                    break;
                case 7:
                    saucerSpawn = new Vector3(4f, -3f, 1f);
                    key = saucerSpawn.ToString();
                    saucers.Add(key, Instantiate(saucerPref, saucerSpawn, Quaternion.identity));
                    break;
                case 8:
                    saucerSpawn = new Vector3(5f, -4f, 1f);
                    key = saucerSpawn.ToString();
                    saucers.Add(key, Instantiate(saucerPref, saucerSpawn, Quaternion.identity));
                    break;
                case 9:
                    saucerSpawn = new Vector3(6f, -5f, 1f);
                    key = saucerSpawn.ToString();
                    saucers.Add(key, Instantiate(saucerPref, saucerSpawn, Quaternion.identity));
                    break;
                default:
                    break;
            }
        }
    }


    public void LoseLife()
    {
        lives--;
        ResetLevel();
        if(lives == 0)
        {
            ResetGame();
            curLevelTarget.GetComponent<Cube>().TargetCubeReset();
        }
        UpdateUI();
    }

}



//Lerp (Time from point a to point b)
