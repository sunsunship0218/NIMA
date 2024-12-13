using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAttackingState : PlayerBaseState
{
    public PlayerAttackingState(PlayerStateMachine playerStateMachine, int attackIndex) : base(playerStateMachine)
    {
        attack = playerStateMachine.Attacks[attackIndex];
    }
    Attack attack;
    float previousFrameTime;
    bool alreadyApplyForce;
    public override void Enter()
    {
        Debug.Log("Enter ATK");
        
        //攻擊傷害判定
        playerStateMachine.RightweaponDamage.SetAttack(attack.Damage, attack.knockbackRange);
        playerStateMachine.LeftweaponDamage.SetAttack(attack.Damage, attack.knockbackRange);
        playerStateMachine.animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        Debug.Log($"Enter ATK with AnimationName: {attack.AnimationName}, " +
     $"TransitionDuration: {attack.TransitionDuration}," +
     $"AttackTime:{attack.ComboAttackTime}");
    }
    public override void Update(float deltatime)
    {

        Facetarget();
        FaceEnemy();
        //移動
        MoveWithDeltatime(deltatime);
        //進行攻擊跟狀態判定
        float NormalizedTime = GetNormalizedTime(playerStateMachine.animator,"Attack");
        Debug.Log("NormalizedTime: "+NormalizedTime);
        if (NormalizedTime >=0 && NormalizedTime < 1f)
        {
            if (NormalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if (playerStateMachine.playerInputHandler.isAttacking)
            {
                TryComboAttack(NormalizedTime);
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
        Debug.Log("Exit ATK");
    }
    void TryApplyForce()
    {
        if (alreadyApplyForce) { return; }
        playerStateMachine.forceReceiver.AddForce(playerStateMachine.transform.forward * attack.Force);
        alreadyApplyForce = true;
    }
    //Melee攻擊
    void TryComboAttack(float normalizedTime)
    {
        //如果不能做連擊
        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }
       
        //如果可以連擊
        playerStateMachine.SwitchState ( new PlayerAttackingState ( playerStateMachine,attack.ComboStateIndex  ));
    }
}