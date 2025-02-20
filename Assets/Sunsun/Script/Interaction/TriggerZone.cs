using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    BossStateMachine bossStateMachine;
    [SerializeField] GameObject backgroundMusic;
    private void Awake()
    {
        bossStateMachine=FindObjectOfType<BossStateMachine>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            backgroundMusic.SetActive(true);
           bossStateMachine.inTheZone = true;
          
        }

    }
}
