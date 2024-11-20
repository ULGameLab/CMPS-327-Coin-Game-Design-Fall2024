using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private GameObject parentObject;
    public int maxEnemyCount = 25;
    public float secondsBetweenSpawn = 2;

    float elapsedTime = 0;
    int currentEnemyCount = 0;
    int numberofEnemyPrefabs = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyCount = CountEnemy();
        parentObject = transform.gameObject;
        numberofEnemyPrefabs = enemyPrefabs.Length;

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > secondsBetweenSpawn && currentEnemyCount < maxEnemyCount)
        {
            elapsedTime = 0;
            Vector3 spawnPosition = RandomPositionAroundPlayer();
            int randomIndex = Random.Range(0, numberofEnemyPrefabs);
            GameObject newEnemy = (GameObject)Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.Euler(0, 0, 0));
            newEnemy.transform.SetParent(parentObject.transform);
            currentEnemyCount = CountEnemy();
        }

    }

    private int CountEnemy()
    {
        int count = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        count = enemies.Length;
        return count;
    }

    private Vector3 RandomPositionAroundPlayer()
    {
        Vector3 randPos = new Vector3(Random.Range(-30.0f, 30.0f), 0, Random.Range(-30.0f, 30.0f));
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        randPos += playerPos;
        randPos.x += 10.0f;
        randPos.y = 0.11f;
        randPos.z += 10.0f;
        return randPos;
    }

}
