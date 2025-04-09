using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTriggerZone : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;

    [SerializeField] float delayEnemy1 = 0f;   // 進入後馬上出現
    [SerializeField] float delayEnemy2 = 3f;   // 再過 3 秒
    [SerializeField] float delayEnemy3 = 6f;   // 再過 6 秒
    bool hasTriggered = false;
    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
         
            StartCoroutine(SpawnRoutine());


        }

    }
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(delayEnemy1);
        if (enemy1 != null) enemy1.SetActive(true);

        yield return new WaitForSeconds(delayEnemy2 - delayEnemy1);
        if (enemy2 != null) enemy2.SetActive(true);

        yield return new WaitForSeconds(delayEnemy3 - delayEnemy2);
        if (enemy3 != null) enemy3.SetActive(true);
    }
}
