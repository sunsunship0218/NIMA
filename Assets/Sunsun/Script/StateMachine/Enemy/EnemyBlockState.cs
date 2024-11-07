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
       
        //�����ʵe�������
        enemyStatemachine.animator.CrossFadeInFixedTime(BlockHASH, TransitionDuration);
    }
    public override void Update(float deltaTime)
    {
        //�����ʵe����
        if (GetNormalizedTime(enemyStatemachine.animator) >= 1)
        {
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
        }

    }
    public override void Exit()
    {

    }
}
