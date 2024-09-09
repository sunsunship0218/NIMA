using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   public  HealthSystem healthSystem;
    [SerializeField] GameManager gameManager;
    void Awake()
    {
        healthSystem = new HealthSystem(gameManager.player.playerHP);
    }

    
}
