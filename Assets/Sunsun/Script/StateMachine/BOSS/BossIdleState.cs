using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossIdleState : BossBaseState
{
    readonly int IdleHASH = Animator.StringToHash("Idle");
    readonly int AnticipationHASH = Animator.StringToHash("Anticipation");
    const float crossfadeDuration = 0.1f;
    const float animatorDampSpeed = 0.14f;
    //前搖的Flag
    bool isAnticipation;
    public BossIdleState(BossStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    public override void Enter()
    {
        isAnticipation = false;
        enemyStatemachine.animator.CrossFadeInFixedTime(IdleHASH, crossfadeDuration);
    }
    public override void Update(float deltaTime)
    {
        MoveWithDeltatime(deltaTime);
        /* if (IsInChasingRange())
         {
             enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
         }
        */
        if (enemyStatemachine.inTheZone && !isAnticipation)
        {
            isAnticipation = true;
            enemyStatemachine.animator.CrossFadeInFixedTime(AnticipationHASH, crossfadeDuration);
        }
        if (isAnticipation) 
        {
            AnimatorStateInfo currentStateInfo = enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
            bool isAttackAnimationFinished = currentStateInfo.normalizedTime > 0.95f && currentStateInfo.IsName("Anticipation");
            if (isAttackAnimationFinished && IsinAttackingRange())
            {
                Debug.Log("前搖結束");
                Debug.Log("IN BOSS ATK RANGE");
                enemyStatemachine.SwitchState(new BossAttackingState(enemyStatemachine, 0));
            }
        /*    if (IsinAttackingRange())
            {
                
                Debug.Log(currentStateInfo.normalizedTime);
                //
            }
        */
        }

        FacePlayer();
    }
    public override void Exit()
    {

    }

  
}
