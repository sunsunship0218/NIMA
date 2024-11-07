using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    //componment
    [field: SerializeField]
    public Animator animator { get; private set; }

    [field: SerializeField]
    public NavMeshAgent agent { get; private set; }

    [field: SerializeField]
    public CharacterController characterController { get; private set; }
    [field: SerializeField]
    public RagDoll ragDoll { get; private set; }
    //目標
    [field: SerializeField]
    public Target target { get; private set; }
    //血量
    [field: SerializeField]
    public  EnemyHealth health { get; private set; }

    //攻擊相關的參數
    //處理左手攻擊
    [field: SerializeField]
    public WeaponDamage weaponDamageL { get; private set; }
    //處理右手攻擊
    [field: SerializeField]
    public WeaponDamage weaponDamageR { get; private set; }

    [field: SerializeField]
    public float AttackRange { get; private set; }

    [field: SerializeField]
    public int AttackingDamage { get; private set; }
    //擊退距離
    [field: SerializeField]
    public int KnockBack { get; private set; } = -1;
    //距離偵測的參數
    [field: SerializeField]
   public float detectionPlayerRange { get; private set; }
    //移動參數
    [field: SerializeField]
    public float MovementSpeed { get; private set; }

    [field: SerializeField]
    public ForceReceiver forceReceiver { get; private set; }
    //玩家
    public GameObject player { get; private set; }
    public PlayerStateMachine playerStateMachine;
    void Start()
    {
        player =  GameObject.FindGameObjectWithTag("Player");
      //  playerStateMachine = player.GetComponentInChildren<PlayerStateMachine>();
        //關掉自動旋轉跟更新路徑,改以手動設定
        agent.updatePosition = false;
        agent.updateRotation = false;
        //this 就是現在的PlayerStateMachine實例
        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionPlayerRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

 
    private void OnEnable()
    {
        Debug.Log("Enemy HandleTakeDamage");
        health.healthSystem.OnTakeDamage += HandleTakeDamage;
        health.healthSystem.OnDie += HandleDie;
    }

    private void OnDisable()
    {
       health.healthSystem.OnTakeDamage -= HandleTakeDamage;
        health.healthSystem.OnDie -= HandleDie;
    }

    //敵人事件沒有被訂閱
    void HandleTakeDamage()
    {
       
        SwitchState(new EnemyImpactState (this));
    }
    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }
}
