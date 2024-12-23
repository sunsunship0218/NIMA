using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyATKManager : MonoBehaviour
{
    List<GameObject> EnemyInRange;
    public void AddEnemyInRange(GameObject enemy)
    {
        if(!EnemyInRange.Contains(enemy))
        EnemyInRange.Add(enemy);
    }
    public  void RemoveEnemyInRange(GameObject enemy)
    {
        EnemyInRange.Remove(enemy);
    }
}
