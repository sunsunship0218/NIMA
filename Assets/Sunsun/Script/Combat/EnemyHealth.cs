using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public HealthSystem healthSystem;
    void Awake()
    {
        healthSystem = new HealthSystem(100, 0);
    }

}
