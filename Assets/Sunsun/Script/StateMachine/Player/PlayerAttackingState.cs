using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    public PlayerAttackingState(PlayerStateMachine playerStateMachine , int attackIndex) : base(playerStateMachine)
    {
        attack = playerStateMachine.Attacks[attackIndex];
    }
    Attack  attack;
    float previousFrameTime;
    bool alreadyApplyForce;
    public override void Enter()
    {
      
        playerStateMachine.animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
   //   playerStateMachine.PlayTrail();
    }
    public override void Update(float deltatime)
    {
        //移動
       MoveWithDeltatime(deltatime);    
        //進行攻擊跟狀態判定
        float NormalizedTime = GetNormalizedTime();
        if(NormalizedTime >= previousFrameTime && NormalizedTime < 1f)
        {
            if(NormalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            previousFrameTime = NormalizedTime;
            if (playerStateMachine.playerInputHandler.isAttacking)
            {
                TryComboAttack(NormalizedTime);
                Facetarget();
            }
        }
        else
        {
            if (playerStateMachine.targeter.currentTarget != null)
            {
                playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
            }
            else
            {
                playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
            }
        }
        previousFrameTime = NormalizedTime;
    }
    public override void Exit()
    {

    }

    void TryApplyForce()
    {
        if(alreadyApplyForce) { return; }
        playerStateMachine.forceReceiver.AddForce(playerStateMachine.transform.forward * attack.Force);
        alreadyApplyForce = true;
    }
    //Melee攻擊
    void TryComboAttack(float normalizedTime)
    {
        //如果不能做連擊
        if (attack.ComboStateIndex == -1)
        {
            return;
        }
        //如果可以連擊
        if(normalizedTime< attack.ComboAttackTime) { return; }

        playerStateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                playerStateMachine,
                attack.ComboStateIndex
            )
        );
    }
    //計算動畫切換的時間
    float GetNormalizedTime()
    {
       AnimatorStateInfo currentStateInfo= playerStateMachine.animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo NextStateInfo= playerStateMachine.animator.GetNextAnimatorStateInfo(0);
        if (playerStateMachine.animator.IsInTransition(0) && NextStateInfo.IsTag("Attack"))
        {
            return NextStateInfo.normalizedTime;
        }
        else if(!playerStateMachine.animator.IsInTransition(0) && currentStateInfo.IsTag("Attack"))
        {
            return currentStateInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }

}
