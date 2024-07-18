using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NIMA.UI;
public class PlayerHealth : MonoBehaviour
{
   HealthSystem healthSystem;
    //做成prefab
    HealthBar healthBar;
    //Transform HealthBarTrans = Instantiate(prefab物件名稱, new Vector3, Quantanion.identy)
    //HealthBar healthBar = HealthBarTrans.GetCOmponment<HealthBar>();
    void Start()
     {
        healthBar.Setup(healthSystem);
        healthSystem = new HealthSystem(100);
        Debug.Log("HEalth : " + healthSystem.GetHealth());
        healthSystem.Damage(30);
        Debug.Log("HEalth : " + healthSystem.GetHealth());
        healthSystem.HealAmount(20);
        Debug.Log("HEalth : " + healthSystem.GetHealth());

        Debug.Log("healthpercent : " + healthSystem.GetHealthPerscent()); 

     }
}
