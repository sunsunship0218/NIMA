using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] Collider myColi;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] TimeManager timeManager;
    [SerializeField] HitParticleEffect hitParticleEffect;
    [SerializeField] AudioSource audioSource;
    List<Collider> alreadyColiWith =new List<Collider>();
    int damage;
    float knockback;

    private void OnEnable()
    {
        alreadyColiWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == myColi) { return; }
        if (alreadyColiWith.Contains(other)) { return; }
        if(other.tag!="Player" && other.tag != "Enemy") { return; }
        alreadyColiWith.Add(other);

        if (other.tag == "Enemy")
        {
            Vector3 hitposition = other.ClosestPointOnBounds(transform.position);
            hitParticleEffect.PlayHitParticle(hitposition);
            timeManager.DoBulletTime(0.4f);
            enemyHealth.healthSystem.Damage(damage);
            audioSource.Play();
      //    Debug.Log("enemy HP :"+enemyHealth.healthSystem.GetHealth());
        }
        if (other.tag == "Player")
        {
            audioSource.Play();
            Vector3 hitposition = other.ClosestPointOnBounds(transform.position);
            hitParticleEffect.PlayHitParticle(hitposition);
            playerHealth.healthSystem.Damage(damage);
            timeManager.DoBulletTime(0.2f);
            
            
            // Debug.Log("player HP :"+playerHealth.healthSystem.GetHealth());
        }

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            //�ھ����h�Z��,�Ӭݻݭn�h�h��
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
