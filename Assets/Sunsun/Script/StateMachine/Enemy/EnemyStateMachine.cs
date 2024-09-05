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

    //�����������Ѽ�
    [field: SerializeField]
    public WeaponDamage weaponDamageL { get; private set; }

    [field: SerializeField]
    public WeaponDamage weaponDamageR { get; private set; }

    [field: SerializeField]
    public float AttackRange { get; private set; }

    [field: SerializeField]
    public int AttackingDamage { get; private set; }

    //�Z���������Ѽ�
    [field: SerializeField]
   public float detectionPlayerRange { get; private set; }
    //���ʰѼ�
    [field: SerializeField]
    public float MovementSpeed { get; private set; }

    [field: SerializeField]
    public ForceReceiver forceReceiver { get; private set; }
    //���a
    public GameObject player { get; private set; }

    void Start()
    {
         player =  GameObject.FindGameObjectWithTag("Player");
        //�����۰ʱ�����s���|,��H��ʳ]�w
        agent.updatePosition = false;
        agent.updateRotation = false;
        //this �N�O�{�b��PlayerStateMachine���
        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionPlayerRange);
    }
}
