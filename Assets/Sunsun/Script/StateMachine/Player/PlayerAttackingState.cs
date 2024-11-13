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
        //�n�̬۾�
      //  CinemachineShake.Instance.ShakeCamera(2f, 1f);
        //�����ˮ`�P�w
        playerStateMachine.RightweaponDamage.SetAttack(attack.Damage, attack.knockbackRange);
        playerStateMachine.LeftweaponDamage.SetAttack(attack.Damage, attack.knockbackRange);
        playerStateMachine.animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
   //   playerStateMachine.PlayTrail();
    }
    public override void Update(float deltatime)
    {
        Facetarget();
        //����
       MoveWithDeltatime(deltatime);    
        //�i������򪬺A�P�w
        float NormalizedTime = GetNormalizedTime(playerStateMachine.animator);
        if(NormalizedTime >= previousFrameTime && NormalizedTime < 1f)
        {
            if(NormalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            previousFrameTime = NormalizedTime;
            //���U������
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
    //Melee����
    void TryComboAttack(float normalizedTime)
    {
        //�p�G���వ�s��
        if (attack.ComboStateIndex == -1)
        {
            return;
        }
        if(normalizedTime< attack.ComboAttackTime) { return; }
        //�p�G�i�H�s��
        playerStateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                playerStateMachine,
                attack.ComboStateIndex
            )
        );
    }
    //�p��ʵe�������ɶ�
  

}
