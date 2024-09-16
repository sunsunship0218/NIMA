using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] Collider myColi;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] TimeManager timeManager;
    List<Collider> alreadyColiWith =new List<Collider>();
    int damage;
    float knockback;

    private void OnEnable()
    {
        alreadyColiWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //避免重複碰撞扣血
        if (other == myColi) { return; }
        if (alreadyColiWith.Contains(other)) { return; }
        if(other.tag!="Player" && other.tag != "Enemy") { return; }
        alreadyColiWith.Add(other);

        // herw is bug
        if (other.tag == "Enemy")
        {
           // timeManager.DoBulletTime(0.1f);
            enemyHealth.healthSystem.Damage(damage);
            Debug.Log("enemy HP :"+enemyHealth.healthSystem.GetHealth());
        }
        if (other.tag == "Player")
        {
          //  timeManager.DoBulletTime(1f);
            playerHealth.healthSystem.Damage(damage);
            Debug.Log("player HP :"+playerHealth.healthSystem.GetHealth());
        }

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            //根據擊退距離,來看需要退多遠
            Vector3 direction = (other.transform.position - myColi.transform.position).normalized;
            forceReceiver.AddForce(direction*knockback);
        }
    }

    public  void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
