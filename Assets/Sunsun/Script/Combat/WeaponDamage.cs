using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] Collider myColi;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] EnemyHealth enemyHealth;
    List<Collider> alreadyColiWith =new List<Collider>();
    int damage;

    private void OnEnable()
    {
        alreadyColiWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
     
        //Á×§K­«½Æ¸I¼²¦©¦å
        if (other == myColi) { return; }
        if (alreadyColiWith.Contains(other)) { return; }
        if(other.tag!="Player" && other.tag != "Enemy") { return; }
        alreadyColiWith.Add(other);

        if (other.tag == "Enemy")
        {
            enemyHealth.healthSystem.Damage(damage);
            Debug.Log("enemy HP :"+enemyHealth.healthSystem.GetHealth());
        }
        if (other.tag == "Player")
        {
            Debug.Log("player hit");
            playerHealth.healthSystem.Damage(damage);
            Debug.Log("player HP :"+playerHealth.healthSystem.GetHealth());
        }
    }

    public  void SetAttack(int damage)
    {
        Debug.Log("IS SET ATTACK");
        this.damage = damage;
        Debug.Log("SetAttack called with damage: " + damage);
    }
}
