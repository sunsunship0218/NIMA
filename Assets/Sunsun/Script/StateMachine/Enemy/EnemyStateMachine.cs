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
    //�ؼ�
    [field: SerializeField]
    public Target target { get; private set; }
    //��q
    [field: SerializeField]
    public  EnemyHealth health { get; private set; }

    //�����������Ѽ�
    //�B�z�������
    [field: SerializeField]
    public WeaponDamage weaponDamageL { get; private set; }
    //�B�z�k�����
    [field: SerializeField]
    public WeaponDamage weaponDamageR { get; private set; }
    //����combo
    [field: SerializeField]
    public Attack[] Attacks { get; private set; }
    //�����Z��
    [field: SerializeField]
    public float AttackRange { get; private set; }
    //¶��Z��
    [field: SerializeField]
    public float CirclingAroundRange {  get; private set; }
    [field: SerializeField]
    public int AttackingDamage { get; private set; }
    //�Q����������
    public int hitCount ;
    //�Q�������ɶ�
    public float lastHitTime;
    //
    public float lastCirclingTime;
    //���s�ɶ�
    [field: SerializeField]
    public float hitCountResetTime { get;private set; }
    //���h�Z��
    [field: SerializeField]
    public int KnockBack { get; private set; } = -1;
    //�Z���������Ѽ�
    [field: SerializeField]
   public float detectionPlayerRange { get; private set; }
    //���ʰѼ�
    [field: SerializeField]
    public float MovementSpeed { get; private set; }
    [field: SerializeField]
    public float circlingSpeed {  get; private set; }
    [field: SerializeField]
    public ForceReceiver forceReceiver { get; private set; }
    //��h
    [field: SerializeField]
    public float RetreatRange { get; private set; }
    //���a
    public GameObject player;
    public Vector3 playerPosition;
    public PlayerStateMachine playerStateMachine;
    //���A���Ѽ�
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
        //���A��Ȫ�l��
        IdleState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
    CirclingState = new EnemyChasingState(this);
        AttackingState = new EnemyAttackingState(this, 0);
        BlockState = new EnemyBlockState(this);
      //  RetreatState = new EnemyRetreatState(this);
        DeadState = new EnemyDeadState(this);
        ImpactState = new EnemyImpactState(this);
        //Get componment
        player =  GameObject.FindGameObjectWithTag("Player");
       playerStateMachine = player.GetComponentInChildren<PlayerStateMachine>();
        //�����۰ʱ�����s���|,��H��ʳ]�w
        agent.updatePosition =false;
        agent.updateRotation = false;
        //this �N�O�{�b��PlayerStateMachine���
        SwitchState(new EnemyIdleState(this));
    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionPlayerRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, CirclingAroundRange);
    }

 
    private void OnEnable()
    {
        Debug.Log("Enemy HandleTakeDamage");
        if (health != null && health.healthSystem != null)
        {
            Debug.Log("�q�\handle Take damage");
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

    void HandleTakeDamage()
    {
       
        SwitchState(new EnemyImpactState (this));
    }
    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }
}
