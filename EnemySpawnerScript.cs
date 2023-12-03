using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public float spawnRate;
    public int maxEnemies;
    public Transform playerTransform;
    public double minDistanceFromPlayer = 2.0;
    private int currentEnemiesNumber = 0;
    private float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if ((currentEnemiesNumber < maxEnemies) && !gameOverScreen.activeSelf && !winScreen.activeSelf)
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;   
            }
            else
            {
                SpawnEnemy();
                timer = 0;
            }
        }
        else
        {
            if (gameOverScreen.activeSelf || winScreen.activeSelf)
            {
                DestroyEnemy();   
            }
        }   
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = new Vector3(Random.Range(10, -10), Random.Range(7, -7), 0);
        } while (Vector3.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer);

        Instantiate(enemy, spawnPosition, Quaternion.identity);
        currentEnemiesNumber++;
    }

    private void DestroyEnemy()
    {
        DestroyImmediate(GameObject.Find("ENEMY(Clone)"), true);
    }
}
