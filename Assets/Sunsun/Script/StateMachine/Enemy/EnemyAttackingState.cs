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
     enemyStatemachine.animator.CrossFadeInFixedTime(AttackHASH,TransitionDuration);
    }
    public override void Update(float deltaTime)
    {
      
    }
    public override void Exit()
    {

    }
 }
