using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NIMA.UI;
public class PlayerHealth : MonoBehaviour
{
   HealthSystem healthSystem;
  [SerializeField] HealthBar healthBar;


    void Start()
     {
 
      //  healthBar.Setup(healthSystem);
        healthSystem = new HealthSystem(100);
        //­q¾\
        healthSystem.onHealthChange += healthBar__onHealthChange;

        Debug.Log("HEalth : " + healthSystem.GetHealth());

        healthBar.SetSliderMax(healthSystem.GetHealth());
        healthBar.SetSlider(healthSystem.GetHealth());

        healthSystem.Damage(30);
        Debug.Log("HEalth : " + healthSystem.GetHealth());

        healthSystem.HealAmount(20);
        Debug.Log("HEalth : " + healthSystem.GetHealth());

        Debug.Log("healthpercent : " + healthSystem.GetHealthPerscent());


    }

   void healthBar__onHealthChange(object sender , System.EventArgs e)
   {
        healthBar.SetSlider(healthSystem.GetHealthPerscent());
    }
 
}
