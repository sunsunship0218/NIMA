using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   public  HealthSystem healthSystem;
    [SerializeField] GameManager gameManager;
    void Start()
    {
        healthSystem = new HealthSystem(gameManager.player.playerHP);
        Debug.Log(healthSystem.GetHealth());
    }

    
}
