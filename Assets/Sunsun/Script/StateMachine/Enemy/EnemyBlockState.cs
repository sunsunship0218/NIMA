using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlockState : EnemyBaseState
{
    readonly int BlockHASH = Animator.StringToHash("Block");
    const float TransitionDuration = 0.1f;
    public EnemyBlockState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    public override void Enter()
    {
       
        //攻擊動畫撥放切換
        enemyStatemachine.animator.CrossFadeInFixedTime(BlockHASH, TransitionDuration);
    }
    public override void Update(float deltaTime)
    {
        //完成動畫撥放
        if (GetNormalizedTime(enemyStatemachine.animator) >= 1)
        {
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
        }

    }
    public override void Exit()
    {

    }
}
