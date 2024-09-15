using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    readonly int AttackHASH = Animator.StringToHash("Attack");
    const float TransitionDuration = 0.1f;
    public EnemyAttackingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    Attack attack;
    public override void Enter()
    {
        //攻擊傷害判定
        enemyStatemachine.weaponDamageL.SetAttack(enemyStatemachine.AttackingDamage, enemyStatemachine.KnockBack);
        //攻擊傷害判定
        enemyStatemachine.weaponDamageR.SetAttack(enemyStatemachine.AttackingDamage, enemyStatemachine.KnockBack);
        //攻擊動畫撥放切換
        enemyStatemachine.animator.CrossFadeInFixedTime(AttackHASH,TransitionDuration);
    }
    public override void Update(float deltaTime)
    {
        //完成動畫撥放
        if (GetNormalizedTime(enemyStatemachine.animator)>=1)
        {
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
        }
       
    }
    public override void Exit()
    {
     
    }
 }
