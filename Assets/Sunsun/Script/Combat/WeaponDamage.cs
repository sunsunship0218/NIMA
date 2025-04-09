using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponDamage : MonoBehaviour
{
    [SerializeField] Collider myColi;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] List<EnemyHealth> enemyHealthList;
    EnemyHealth enemyHealth;
    [SerializeField] TimeManager timeManager;
    [SerializeField] HitParticleEffect hitParticleEffect;
    [SerializeField] AudioSource audioSource;
    List<Collider> alreadyColiWith =new List<Collider>();

    // 定義一個事件，當 enemy 被攻擊時觸發
    public static event Action OnEnemyHit;

    int damage;
    float knockback;

   void Awake()
    {
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        enemyHealthList = new List<EnemyHealth>(enemies);
        playerHealth =FindObjectOfType<PlayerHealth>();
    }
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

        if (other.tag =="Enemy")
        {
            EnemyStateMachine enemyStateMachine = other.GetComponent<EnemyStateMachine>();
            enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyStateMachine != null)
            {
                // 增加被擊中敵人的 hitCount
                enemyStateMachine.hitCount++;
                // 更新被擊中敵人的 lastHitTime
                enemyStateMachine.lastHitTime = Time.time;
                //觸發事件
                OnEnemyHit?.Invoke();

            }
            Vector3 hitposition = other.ClosestPointOnBounds(transform.position);
            //播放特效
            timeManager.DoBulletTime(0.01f);
         
            if (enemyHealth != null)
            {
                hitParticleEffect.PlayHitParticle(hitposition);
                audioSource.Play();
                enemyHealth.healthSystem.Damage(damage);
                //回血攻擊
              //  playerHealth.healthSystem.HealAmount(10);
                
             
            }
             //Debug.Log("enemy HP :"+enemyHealth.healthSystem.GetHealth());
        }
        if (other.tag == "Player")
        {
            
            audioSource.Play();
            Vector3 hitposition = other.ClosestPointOnBounds(transform.position); 
            playerHealth.healthSystem.Damage(damage);
        
            
            
            // Debug.Log("player HP :"+playerHealth.healthSystem.GetHealth());
        }

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
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