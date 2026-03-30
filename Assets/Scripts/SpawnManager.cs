using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int totalSpawnEnemies;
    public int numberOfRandomSpawnPoint;
    public float delayStart;
    public float spawnInterval;
    public int numberOfPowerUp;
}

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    public Wave[] waves;

    private int aliveEnemies = 0;

    void Start()
    {
        StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
        for (int w = 0; w < waves.Length; w++)
        {
            Wave wave = waves[w];

            Debug.Log("Start Wave " + (w + 1));

            //reset 
            aliveEnemies = 0;

            //random spawn points
            int count = Mathf.Min(wave.numberOfRandomSpawnPoint, spawnPoints.Length);

            List<Transform> tempList = new List<Transform>(spawnPoints);
            Transform[] selectedPoints = new Transform[count];

            for (int i = 0; i < count; i++)
            {
                int rand = Random.Range(0, tempList.Count);
                selectedPoints[i] = tempList[rand];
                tempList.RemoveAt(rand);
            }

            //spawn powerups
            for (int i = 0; i < wave.numberOfPowerUp; i++)
            {
                int index = Random.Range(0, spawnPoints.Length);
                Instantiate(powerUpPrefab,
                    spawnPoints[index].position,
                    Quaternion.identity);
            }

            //delay before spawn
            yield return new WaitForSeconds(wave.delayStart);

            //spawn enemies
            for (int i = 0; i < wave.totalSpawnEnemies; i++)
            {
                int index = Random.Range(0, selectedPoints.Length);
                Transform spawnPoint = selectedPoints[index];

                GameObject enemy = Instantiate(enemyPrefab,
                    spawnPoint.position + Vector3.up * 2f,
                    Quaternion.identity);

                if (enemy != null)
                {
                    aliveEnemies++;

                    Enemy e = enemy.GetComponent<Enemy>();
                    if (e != null)
                    {
                        e.SetManager(this);
                    }
                }

                yield return new WaitForSeconds(wave.spawnInterval);
            }

            //wait until all enemies die
            while (aliveEnemies > 0)
            {
                yield return null;
            }

            Debug.Log("Wave " + (w + 1) + " Complete");
        }

        Debug.Log("All Waves Done");
    }

    public void EnemyDied()
    {
        aliveEnemies--;

        if (aliveEnemies < 0)
            aliveEnemies = 0;

        Debug.Log("Enemy left: " + aliveEnemies);
    }
}