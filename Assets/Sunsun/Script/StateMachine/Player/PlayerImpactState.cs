using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    public PlayerImpactState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    readonly int ImpactHASH = Animator.StringToHash("Impact");
    const float crossfadeDuration =0.14f;
    float duration;
    public override void Enter()
    {
     
        playerStateMachine.animator.CrossFadeInFixedTime(ImpactHASH, crossfadeDuration);
       
    }
    public override void Update(float deltaTime)
    {
        MoveWithDeltatime(deltaTime);
        //影響的時間結束
        duration -= deltaTime;
        if (duration <= 0f)
        {
            //避免撥放到一半,馬上切換到Locomotion
            if (playerStateMachine.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f)
            {
                ReturntoLocomotion();
            }
        }
    }
    public override void Exit()
    {

    }
}
