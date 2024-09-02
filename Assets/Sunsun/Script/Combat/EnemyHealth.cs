using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public HealthSystem healthSystem;
    void Start()
    {
        healthSystem = new HealthSystem(100);
        Debug.Log(healthSystem.GetHealth());
    }

}
