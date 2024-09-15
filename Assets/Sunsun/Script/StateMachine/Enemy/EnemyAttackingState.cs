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
        //�����ˮ`�P�w
        enemyStatemachine.weaponDamageL.SetAttack(enemyStatemachine.AttackingDamage, enemyStatemachine.KnockBack);
        //�����ˮ`�P�w
        enemyStatemachine.weaponDamageR.SetAttack(enemyStatemachine.AttackingDamage, enemyStatemachine.KnockBack);
        //�����ʵe�������
        enemyStatemachine.animator.CrossFadeInFixedTime(AttackHASH,TransitionDuration);
    }
    public override void Update(float deltaTime)
    {
        //�����ʵe����
        if (GetNormalizedTime(enemyStatemachine.animator)>=1)
        {
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
        }
       
    }
    public override void Exit()
    {
     
    }
 }
