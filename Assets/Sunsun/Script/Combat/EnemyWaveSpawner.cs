using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] EnemyObjectPool pool;
    // �]�� WAVE
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int enemiesToSpawn = 3;
    //�s�ͦ����󪺦a��
    private Transform container;

    void Start()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 pos = spawnPoint.position + new Vector3(i * 2, 0, 0); // �C���ĤH���Z 2 ���
            pool.GetFromPool(pos);
        }
    }
}
