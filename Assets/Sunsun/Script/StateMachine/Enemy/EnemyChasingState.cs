using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    readonly int LocomotionBlendtreeHASH = Animator.StringToHash("locomotion");
    readonly int SpeedHASH = Animator.StringToHash("Speed");
    const float crossfadeDuration = 0.1f;
    const float animatorDampSpeed = 0.14f;
    public override void Enter()
    {
       enemyStatemachine.animator.CrossFadeInFixedTime(LocomotionBlendtreeHASH, crossfadeDuration);
    }
    public override void Update(float deltaTime)
    {
        if (!IsInChasingRange())
        {
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
            return;
        }
        else if(IsinAttackingRange())
        {
            enemyStatemachine.SwitchState(new EnemyAttackingState(enemyStatemachine));
        }
        // In chasing range
        MoveToPlayer(deltaTime);
        FacePlayer();
        enemyStatemachine.animator.SetFloat(SpeedHASH, 1f, animatorDampSpeed, deltaTime);
    }
    public override void Exit()
    {
        //���]���|
      enemyStatemachine.agent.ResetPath();
      enemyStatemachine.agent.velocity = Vector3.zero;
    }

    void MoveToPlayer(float deltatime)
    {
        if (enemyStatemachine.agent.isOnNavMesh)
        {
            //���ʨ쪱�a�Ҧb���a�I
            enemyStatemachine.agent.destination = enemyStatemachine.player.transform.position;
            Move(enemyStatemachine.agent.desiredVelocity.normalized * enemyStatemachine.MovementSpeed, deltatime);
        }
       
        //AI���t�װѼƸ�
        enemyStatemachine.agent.velocity =enemyStatemachine.characterController.velocity;
    }
}