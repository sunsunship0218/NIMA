using System.Collections;
using System.Collections.Generic;
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
        //更新現在血量
        health = playerHealth.healthSystem.GetHealth();
        IMGEffect();
    }

    void IMGEffect()
    {
        float transparency = 1f -(health/maxHealth);
        Color IMGcolor = Color.white;
        IMGcolor.a = transparency;
        HealthimpactIMG.color = IMGcolor;
    }
}
