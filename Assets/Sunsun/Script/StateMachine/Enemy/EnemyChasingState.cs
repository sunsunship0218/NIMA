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
      //  Debug.Log("Retreat? "+ShouldRetreat());
        if (!IsInChasingRange())
        {
          //  Debug.Log("not in chasing range");
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
            return;
        }
        else if (ShouldAttack())
        {
            enemyStatemachine.SwitchState(new EnemyAttackingState(enemyStatemachine,0));
        }
        else if (ShouldBlock())
        {
            enemyStatemachine.SwitchState(new EnemyBlockState(enemyStatemachine));
        }
        else if (ShouldRetreat())
        { 
            enemyStatemachine.SwitchState(new EnemyRetreatState(enemyStatemachine));
        }
        // In chasing range
        MoveToPlayer(deltaTime);
        FacePlayer();
        enemyStatemachine.animator.SetFloat(SpeedHASH, 1f, animatorDampSpeed, deltaTime);
    }
    public override void Exit()
    {
        //重設路徑
      enemyStatemachine.agent.ResetPath();
      enemyStatemachine.agent.velocity = Vector3.zero;
    }

    void MoveToPlayer(float deltatime)
    {
        if (enemyStatemachine.agent.isOnNavMesh)
        {
            //移動到玩家所在的地點
            enemyStatemachine.agent.destination = enemyStatemachine.player.transform.position;
            Move(enemyStatemachine.agent.desiredVelocity.normalized * enemyStatemachine.MovementSpeed, deltatime);
        }
       
        //AI的速度參數跟
        enemyStatemachine.agent.velocity =enemyStatemachine.characterController.velocity;
    }
}
