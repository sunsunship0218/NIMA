using System;
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
        enemyStatemachine.agent.enabled = true;
        enemyStatemachine.animator.applyRootMotion = false;
        enemyStatemachine.agent.updatePosition = false;
        enemyStatemachine.agent.updateRotation = false;
        enemyStatemachine.animator.CrossFadeInFixedTime(LocomotionBlendtreeHASH, crossfadeDuration);
    }
    public override void Update(float deltaTime)
    {
        if (IsinAttackingRange())
        {
                enemyStatemachine.SwitchState(new EnemyAttackingState(enemyStatemachine, 0));
        }
        if (!IsInChasingRange())
        {
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
        }
        if (!IsinAttackingRange())
        {
            MoveToPlayer(deltaTime);
            FacePlayer();
            enemyStatemachine.animator.SetFloat(SpeedHASH, 1f, animatorDampSpeed, deltaTime);
        }


    }
    public override void Exit()
    {
        enemyStatemachine.animator.applyRootMotion = true;
        //重設路徑
        enemyStatemachine.agent.ResetPath();
      enemyStatemachine.agent.velocity = Vector3.zero;

    }

    void MoveToPlayer(float deltatime)
    {
        if (enemyStatemachine.agent.isOnNavMesh)
        {
            //移動到玩家所在的地點
            enemyStatemachine.agent.destination = enemyStatemachine.playerStateMachine.transform.position;
            Move(enemyStatemachine.agent.desiredVelocity.normalized * enemyStatemachine.MovementSpeed, deltatime);
        }
       
        //AI的速度參數跟
        enemyStatemachine.agent.velocity =enemyStatemachine.characterController.velocity;
    }
   



}
