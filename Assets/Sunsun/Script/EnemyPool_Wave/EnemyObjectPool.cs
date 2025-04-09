using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool Instance;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int initialPoolSize = 10;

    Queue<GameObject> pool = new Queue<GameObject>();
    Transform container;

    void Awake()
    {
        Instance = this;
        container = new GameObject("EnemyPool").transform;

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, container);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool()
    {
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(enemyPrefab, container);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
