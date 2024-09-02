using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] Collider playerColi;
    [SerializeField] PlayerHealth playerHealth;
    List<Collider> alreadyColiWith =new List<Collider>();
    int danage;

    private void OnEnable()
    {
        alreadyColiWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Á×§K­«½Æ¸I¼²¦©¦å
        if (other == playerColi) { return; }
        if (alreadyColiWith.Contains(other)) { return; }
        alreadyColiWith.Add(other);
        if (other.tag == "Enemy")
        {
            playerHealth.healthSystem.Damage(10);
            Debug.Log(playerHealth.healthSystem.GetHealth());
        }
     

    }

    public  void SetAttack(int damage)
    {
      this.danage = damage;
    }
}
