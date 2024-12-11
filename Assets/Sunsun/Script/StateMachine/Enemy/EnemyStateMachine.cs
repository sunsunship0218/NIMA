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

    //不同攻擊combo
    [field: SerializeField]
    public Attack[]  ShortAttacks { get; private set; }

    [field: SerializeField]
    public Attack[] MidAttacks { get; private set; }

    [field: SerializeField]
    public Attack[] LongAttacks { get; private set; }
    //攻擊距離
    [field: SerializeField]
    public float AttackRange { get; private set; }
    //決定攻擊動畫撥放的距離
    [field: SerializeField]
    public float LongAttackRange { get; private set; }
    [field: SerializeField]
    public float ShortAttackRange { get; private set; }
    [field: SerializeField]
    public float MidAttackRange { get; private set; }
    //繞行距離
    [field: SerializeField]
    public float CirclingAroundRange {  get; private set; }
    [field: SerializeField]
    public int AttackingDamage { get; private set; }
    //被擊中的次數
    public int hitCount ;
    //被擊中的時間
    public float lastHitTime;
    //
    public float lastCirclingTime;
    //重製時間
    [field: SerializeField]
    public float hitCountResetTime { get;private set; }
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
    public float circlingSpeed {  get; private set; }
    [field: SerializeField]
    public ForceReceiver forceReceiver { get; private set; }
    //後退
    [field: SerializeField]
    public float RetreatRange { get; private set; }
    //玩家
    public GameObject player;
    public Vector3 playerPosition;
    public PlayerStateMachine playerStateMachine;
    //狀態機參數
    public EnemyBaseState IdleState { get; set; }
    public EnemyBaseState CirclingState { get; set; }
    public EnemyBaseState ChasingState { get; set; }
    public EnemyBaseState AttackingState { get; set; }
    public EnemyBaseState BlockState { get; set; }
    public EnemyBaseState RetreatState { get; set; }
    public EnemyBaseState DeadState { get; set; }
    public EnemyBaseState ImpactState { get; set; }
    void Start()
    {
        //狀態賦值初始化
        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        CirclingState = new EnemyCirclingState(this);
        AttackingState = new EnemyAttackingState(this, 0);
        BlockState = new EnemyBlockState(this);
      //  RetreatState = new EnemyRetreatState(this);
        DeadState = new EnemyDeadState(this);
        ImpactState = new EnemyImpactState(this);
        //Get componment
        player =  GameObject.FindGameObjectWithTag("Player");
       playerStateMachine = player.GetComponentInChildren<PlayerStateMachine>();
        //關掉自動旋轉跟更新路徑,改以手動設定
        agent.updatePosition =false;
        agent.updateRotation = false;
        //this 就是現在的PlayerStateMachine實例
        SwitchState(new EnemyIdleState(this));
    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionPlayerRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, CirclingAroundRange);

        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, ShortAttackRange);
        Gizmos.DrawWireSphere(transform.position, MidAttackRange);
        Gizmos.DrawWireSphere(transform.position, LongAttackRange);

    }

 
    private void OnEnable()
    {
        Debug.Log("Enemy HandleTakeDamage");
        if (health != null && health.healthSystem != null)
        {
            Debug.Log("訂閱handle Take damage");
            health.healthSystem.OnTakeDamage += HandleTakeDamage;
            health.healthSystem.OnDie += HandleDie;
        }
        else
        {

            Debug.LogError("Health or HealthSystem is null in OnEnable");
        }
      
    }

    private void OnDisable()
    {
       health.healthSystem.OnTakeDamage -= HandleTakeDamage;
        health.healthSystem.OnDie -= HandleDie;
    }

    private void OnAnimatorMove()
    {
 
        facePlayer();
        Vector3 newPosition = animator.deltaPosition;
        characterController.Move(newPosition);

    }
    void HandleTakeDamage()
    {
       
        SwitchState(new EnemyImpactState (this));
    }
    private void HandleDie()
    {
        Debug.Log("DIE");
        SwitchState(new EnemyDeadState(this));
    }
 void facePlayer()
    {
        if (player == null) { return; }
        Vector3 faceTargetPos;
        faceTargetPos = player.transform.position - this.transform.position;
        faceTargetPos.y = 0f;
       this.transform.rotation = Quaternion.LookRotation(faceTargetPos);
    }
}
