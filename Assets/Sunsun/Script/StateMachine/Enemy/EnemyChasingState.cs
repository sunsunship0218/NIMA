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
       
    }
    public override void Update(float deltaTime)
    {
        if (!IsInChasingRange())
        {
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
            return;
        }
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
        //移動到玩家所在的地點
        enemyStatemachine.agent.destination = enemyStatemachine.player.transform.position;
        Move(enemyStatemachine.agent.desiredVelocity.normalized * enemyStatemachine.MovementSpeed, deltatime);
        //AI的速度參數跟
        enemyStatemachine.agent.velocity =enemyStatemachine.characterController.velocity;
    }
}
