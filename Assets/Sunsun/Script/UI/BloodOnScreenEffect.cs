using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BloodOnScreenEffect : MonoBehaviour
{
    [SerializeField] RawImage HealthimpactIMG;
    [SerializeField] PlayerHealth playerHealth;
    float health;
    float maxHealth;
    private void Awake()
    {
        playerHealth.healthSystem.OnHealthChange += P_HealthSystem_OnHealthChange;
    }
    private void Start()
    {
        maxHealth = playerHealth.healthSystem.GetMaxHealth();
    }
    private void P_HealthSystem_OnHealthChange(object sender, System.EventArgs e)
    {
        //��s�{�b��q
        health = playerHealth.healthSystem.GetHealth();

        IMGEffect();
    }

    void IMGEffect()
    {
        float transparency;
        float ratio = (health / maxHealth);
        Color IMGcolor = Color.white;
        if (ratio > 0.8f)
        {
            transparency = 0f;
        }
        else
        {
            transparency = (0.8f - ratio);
        }
        IMGcolor.a = transparency;
        HealthimpactIMG.color = IMGcolor;
    }
}
