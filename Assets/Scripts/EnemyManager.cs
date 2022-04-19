using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    //private List<Vector3> spawnPoints = new List<Vector3>();
    public int currentLevel;
    public float spawnTime;
    private bool isCoily;
    private int choice;

    private void Start()
    {
        currentLevel = 0;
        isCoily = false;
    }


    IEnumerator SpawnCheck()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            if(currentLevel == 1)
            {
                choice = Random.Range(0, 2);
                enemies.Add(Instantiate(enemyPrefabs[choice]));
            }
            else if(currentLevel == 2)
            {
                spawnTime = 5.5f;
                choice = Random.Range(0, 3);
                if (choice != 2)
                {
                        enemies.Add(Instantiate(enemyPrefabs[choice]));
                }
                else if(choice == 2 && !isCoily)
                {
                        enemies.Add(Instantiate(enemyPrefabs[choice]));
                }
            }
            else
            {
                spawnTime = 4.5f;
                choice = Random.Range(0, enemyPrefabs.Count);
                if(choice == 2 && !isCoily)
                    enemies.Add(Instantiate(enemyPrefabs[choice]));
                else if (choice != 2)
                {
                    enemies.Add(Instantiate(enemyPrefabs[choice]));
                }

            }
            yield return new WaitForSeconds(spawnTime);

        }
    }



    public void EnableSpawner(int currentLevel)
    {
        this.currentLevel = currentLevel;
        StartCoroutine(SpawnCheck());
    }

    public void SpawnCoily(GameObject coily, Vector3 spawn)
    {
        if(!isCoily)
            enemies.Add(Instantiate(coily, spawn, Quaternion.identity));
    }


    public void WipeEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    private void OnApplicationQuit()
    {
        WipeEnemies();
    }
}
