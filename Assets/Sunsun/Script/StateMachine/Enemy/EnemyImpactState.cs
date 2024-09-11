using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    public EnemyImpactState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    readonly int ImpactHASH = Animator.StringToHash("Impact");
    const float crossfadeDuration = 0.14f;
    float duration;
    public override void Enter()
    {
        enemyStatemachine.animator.CrossFadeInFixedTime(ImpactHASH, crossfadeDuration);
    }
    public override void Update(float deltaTime)
    {
        MoveWithDeltatime(deltaTime);
        duration -= deltaTime;
        if(duration < 0)
        {
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
            if (duration <= 0)
            {
                //避免撥放到一半,馬上切換到Locomotion
                if (enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                   enemyStatemachine.SwitchState(new EnemyIdleState (enemyStatemachine));
                }
            }
        }
    }
    public override void Exit()
    {

    }
}
