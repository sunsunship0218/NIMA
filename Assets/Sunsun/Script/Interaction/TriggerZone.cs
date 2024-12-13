using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    BossStateMachine bossStateMachine;
    private void Awake()
    {
        bossStateMachine=FindObjectOfType<BossStateMachine>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           bossStateMachine.inTheZone = true;
            Debug.Log("IntheZone? " + bossStateMachine.inTheZone);
        }

    }
}
