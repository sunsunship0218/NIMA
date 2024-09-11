using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    public Slider healthBar;
    public float maxHealth;
    public float health;
    private void Start()
    {
        maxHealth =playerHealth.healthSystem.GetHealth();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        playerHealth.healthSystem.OnHealthChange += HealthSystem_OnHealthChange;
    }
    private void Update()
    {

    }

    private void HealthSystem_OnHealthChange(object sender, System.EventArgs e)
    {
        Debug.Log("HandleHC");
        //��s�{�b��q
        health =playerHealth.healthSystem.GetHealth();
        //��sUI
        if (healthBar.value != health)
        {
            healthBar.value = health;
        }
    }


}

