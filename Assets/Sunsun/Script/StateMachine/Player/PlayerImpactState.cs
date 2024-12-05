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
        //�v�T���ɶ�����
        duration -= deltaTime;
        if (duration <= 0f)
        {
            //�קK�����@�b,���W������Locomotion
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
