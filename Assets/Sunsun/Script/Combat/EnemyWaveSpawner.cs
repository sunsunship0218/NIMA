using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] EnemyObjectPool pool;
    // 設為 WAVE
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int enemiesToSpawn = 3;
    //存生成物件的地方
    private Transform container;

    void Start()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 pos = spawnPoint.position + new Vector3(i * 2, 0, 0); // 每隻敵人間距 2 單位
            pool.GetFromPool(pos);
        }
    }
}
