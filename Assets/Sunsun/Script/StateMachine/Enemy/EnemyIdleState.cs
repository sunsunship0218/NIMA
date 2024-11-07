using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    readonly int LocomotionBlendtreeHASH = Animator.StringToHash("locomotion");
    readonly int SpeedHASH = Animator.StringToHash("Speed");
    const float crossfadeDuration = 0.1f;
    const float animatorDampSpeed = 0.14f;
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    public override void Enter()
    {
        
        enemyStatemachine.animator.CrossFadeInFixedTime(LocomotionBlendtreeHASH, crossfadeDuration,0);
     
    }
    public override void Update(float deltaTime)
    {
        MoveWithDeltatime(deltaTime);
        if (IsInChasingRange())
        {
            Debug.Log("IsIChasingRange: "+ IsInChasingRange() );
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
            return;
        }
        else if (IsinAttackingRange())
        {
            Debug.Log("IsInAttackingRange: "+IsinAttackingRange() );
            enemyStatemachine.SwitchState(new EnemyAttackingState(enemyStatemachine));
        }
        FacePlayer();
        enemyStatemachine.animator.SetFloat(SpeedHASH, 0f, animatorDampSpeed, deltaTime);
    }
    public override void Exit()
    {
        Debug.Log("exit the idle");
    }
}
