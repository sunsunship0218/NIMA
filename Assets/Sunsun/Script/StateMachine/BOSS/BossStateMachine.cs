using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class BossStateMachine : StateMachine
{
  //  public  event Action OnTigerDestroyed;
    //componment
    [field: SerializeField]
    public Animator animator { get; private set; }

    [field: SerializeField]
    public NavMeshAgent agent { get; private set; }

    [field: SerializeField]
    public CharacterController characterController { get; private set; }

    //�ؼ�
    [field: SerializeField]
    public Target target { get; private set; }
    //��q
    [field: SerializeField]
    public EnemyHealth health { get; private set; }

    //�����������Ѽ�
    //�B�z�������
    [field: SerializeField]
    public WeaponDamage weaponDamageL { get; private set; }
    //�B�z�k�����
    [field: SerializeField]
    public WeaponDamage weaponDamageR { get; private set; }

    //���P����combo
    [field: SerializeField]
    public Attack[] ShortAttacks { get; private set; }

    [field: SerializeField]
    public Attack[] MidAttacks { get; private set; }

    [field: SerializeField]
    public Attack[] LongAttacks { get; private set; }
    //Ĳ�o�i���ʵe�ϰ�
    [field: SerializeField]
    public GameObject TriggerZone { get; private set; }
    //�����Z��
    //�M�w�����ʵe���񪺶Z��
    [field: SerializeField]
    public float LongAttackRange { get; private set; }
    [field: SerializeField]
    public float ShortAttackRange { get; private set; }
    [field: SerializeField]
    public float MidAttackRange { get; private set; }
    //¶��Z��
    [field: SerializeField]
    public float CirclingAroundRange { get; private set; }
    [field: SerializeField]
    public int AttackingDamage { get; private set; }
    //�Q����������
    public int hitCount;
    //�Q�������ɶ�
    public float lastHitTime;
    //
    public float lastCirclingTime;
    //���s�ɶ�
    [field: SerializeField]
    public float hitCountResetTime { get; private set; }
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
    public float circlingSpeed { get; private set; }
    [field: SerializeField]
    public ForceReceiver forceReceiver { get; private set; }
    //��h
    [field: SerializeField]
    public float RetreatRange { get; private set; }
    //���a
    public GameObject player;
    public Vector3 playerPosition;
    public PlayerStateMachine playerStateMachine;
    //Some Flag
  public  bool inTheZone = false;
    public bool inPhase2 ;
    public enum BossPhase
    {
        Phase1,
        Phase2
    }
    //���A���Ѽ�
    private void Start()
    {
        inPhase2 = false;
        //Get componment
        player = GameObject.FindGameObjectWithTag("Player");
        playerStateMachine = player.GetComponentInChildren<PlayerStateMachine>();
        //�����۰ʱ�����s���|,��H��ʳ]�w
        agent.updatePosition = false;
        agent.updateRotation = false;
        //this �N�O�{�b��PlayerStateMachine���
        SwitchState(new BossIdleState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionPlayerRange);
        Gizmos.DrawWireSphere(transform.position, LongAttackRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, MidAttackRange);

        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, ShortAttackRange);



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

    private void OnAnimatorMove()
    {

        facePlayer();
        Vector3 newPosition = animator.deltaPosition;
        characterController.Move(newPosition);

    }

    private void HandleHitCountCheck()
    {
        if (hitCount > 2)
        {
         //   SwitchState(BlockState);
            //���/��h
            hitCount = 0; // ���m?����?
        }
    }
    void HandleTakeDamage()
    {
        CheckPhaseChanging();
      
    }
    void CheckPhaseChanging()
    {
        if (inPhase2 == false && (health.healthSystem.GetHealth() <= health.healthSystem.MaxHealth * 0.5f))
        {

           SwitchState(new BossPhase2TransitionState(this));
        }
        else
        {
            SwitchState(new BossImpactState(this));
        }
    }
    private void HandleDie()
    {
        Debug.Log("DIE");
       // SwitchState(new EnemyDeadState(this));
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
