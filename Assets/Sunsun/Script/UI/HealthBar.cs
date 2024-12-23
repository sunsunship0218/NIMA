using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField]EnemyHealth enemyHealth;
    public Slider healthBar;
    public float maxHealth;
    public float health;
    private void Awake()
    {
        if (playerHealth != null)
            PlayerInitial();

        if (enemyHealth != null)
            EnemyInitial();
    }
   void PlayerInitial()
    {
        maxHealth = playerHealth.healthSystem.GetMaxHealth();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        playerHealth.healthSystem.OnHealthChange += P_HealthSystem_OnHealthChange;
    }
    void EnemyInitial()
    {
        Debug.Log(("Enemy health bar"));
        Debug.Log("Enemy health"+maxHealth);
        Debug.Log("name?"+enemyHealth.gameObject.name);
        maxHealth = enemyHealth.healthSystem.GetHealth();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        enemyHealth.healthSystem.OnHealthChange += E_HealthSystem_OnHealthChange;
    }

    private void P_HealthSystem_OnHealthChange(object sender, System.EventArgs e)
    {
        //更新現在血量
        health =playerHealth.healthSystem.GetHealth();
        //更新UI
        if (healthBar.value != health)
        {
            healthBar.value = health;
        }
    }
    private void E_HealthSystem_OnHealthChange(object sender, System.EventArgs e)
    {

        //更新現在血量
        health = enemyHealth.healthSystem.GetHealth();

        //更新UI
        if (healthBar.value != health)
        {
            healthBar.value = health;
        }
    }


}

