using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public HealthSystem healthSystem;
    void Awake()
    {
        healthSystem = new HealthSystem(100, 0);
        if(this.gameObject.name == "Boss")
        {
            healthSystem = new HealthSystem(200, 0);
        }
    }

}
