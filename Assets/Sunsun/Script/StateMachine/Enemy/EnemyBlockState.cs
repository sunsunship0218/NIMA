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
        AnimatorStateInfo currentStateInfo = enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        //完成動畫撥放
        if (currentStateInfo.normalizedTime>0.8f)
        {
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
        }

    }
    public override void Exit()
    {

    }
}
