using System.Collections;
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

        public Wave[] waves;

        void Start()
        {
            StartCoroutine(WaveRoutine());
        }

        IEnumerator WaveRoutine()
        {
            for (int w = 0; w < waves.Length; w++)
            {
                Wave wave = waves[w];

                //delay
                yield return new WaitForSeconds(wave.delayStart);

                //spawn enemy 
                for (int i = 0; i < wave.totalSpawnEnemies; i++)
                {
                    RandomSpawnLimited(wave.numberOfRandomSpawnPoint);
                    yield return new WaitForSeconds(wave.spawnInterval);
                }

                //waiting enemy
                while (FindObjectsOfType<Enemy>().Length > 0)
                {
                    yield return null;
                }
            }
        }

        void RandomSpawnLimited(int numberOfPoints)
        {
            int index = Random.Range(0, numberOfPoints);
            var spawnPoint = spawnPoints[index];

            Instantiate(enemyPrefab, spawnPoint.position + Vector3.up * 1f, Quaternion.identity);
    }
    }