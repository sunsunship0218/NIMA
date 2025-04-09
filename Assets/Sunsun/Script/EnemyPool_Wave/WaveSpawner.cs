using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float spawnInterval;
    }

    public List<Wave> waves = new List<Wave>();
    public Transform[] spawnPoints;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        while (currentWaveIndex < waves.Count)
        {
            isSpawning = true;
            Wave currentWave = waves[currentWaveIndex];

            for (int i = 0; i < currentWave.enemyCount; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject enemy = EnemyObjectPool.Instance.GetFromPool();
                enemy.transform.position = spawnPoint.position;

                // 初始化（可選）
              //  enemy.GetComponent<Enemy>().Init();

                yield return new WaitForSeconds(currentWave.spawnInterval);
            }

            isSpawning = false;

            // 等待所有敵人死亡後進入下一波
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            currentWaveIndex++;
        }

        Debug.Log("All waves completed!");
    }
}
