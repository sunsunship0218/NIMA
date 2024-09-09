using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    public Slider healthBar;
    public float maxHealth;
    public float health;
    private void Start()
    {
        maxHealth =playerHealth.healthSystem.GetHealth();
        if (healthBar.value != health)
        {
            healthBar.value = health;
        }
    }

}

