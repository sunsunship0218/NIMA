using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2TransitionState : BossBaseState
{
    int TransitionHASH = Animator.StringToHash("Phase2Transition");

    const float crossfadeDuration = 0.1f;
    const float animatorDampSpeed = 0.14f;
    public BossPhase2TransitionState(BossStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    public override void Enter()
    {
        Debug.Log("In phase 2");
        enemyStatemachine.health.healthSystem.SetInvunerable(true);
        enemyStatemachine. inPhase2 = true;
        enemyStatemachine.animator.CrossFadeInFixedTime(TransitionHASH, 0.14f);
    }
    public override void Update(float deltaTime)
    {
        AnimatorStateInfo currentStateInfo = enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        bool isAttackAnimationFinished = currentStateInfo.normalizedTime >= 0.95f && currentStateInfo.IsName("Phase2Transition");
        if (isAttackAnimationFinished) 
        {
            enemyStatemachine.SwitchState(new BossIdleState(enemyStatemachine));
        }
        FacePlayer();
    }
    public override void Exit()
    {
        enemyStatemachine.health.healthSystem.SetInvunerable(false);
    }
}
