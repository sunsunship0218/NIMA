using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField]
    public Animator animator { get; private set; }

    [field: SerializeField]
   public float detectionPlayerRange { get; private set; }

    [field: SerializeField]
    public float AttackRange { get; private set; }

    [field: SerializeField]
    public float MovementSpeed { get; private set; }
    [field: SerializeField]
    public CharacterController characterController { get; private set; }

    [field: SerializeField]
    public ForceReceiver forceReceiver { get; private set; }
    [field: SerializeField]
    public NavMeshAgent agent{ get; private set; }
    public GameObject player { get; private set; }

    void Start()
    {
      player =  GameObject.FindGameObjectWithTag("Player");
        //關掉自動旋轉跟更新路徑
        agent.updatePosition = false;
        agent.updateRotation = false;
        //this 就是現在的PlayerStateMachine實例
        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionPlayerRange);
    }
}
