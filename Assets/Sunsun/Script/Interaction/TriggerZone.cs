using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] BossStateMachine bossStateMachine;
    [SerializeField] GameObject backgroundMusic;
    [SerializeField] GameObject NormalbackgroundMusic;
    [SerializeField] GameObject UI;
    private void Awake()
    {
        bossStateMachine=FindObjectOfType<BossStateMachine>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            backgroundMusic.SetActive(true);
            NormalbackgroundMusic.SetActive(false);
            bossStateMachine.inTheZone = true;
            UI.SetActive(true);
            Debug.Log("In THE BOSS AREA");
          
        }

    }
}
