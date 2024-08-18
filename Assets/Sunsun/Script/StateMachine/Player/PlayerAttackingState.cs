using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    public PlayerAttackingState(PlayerStateMachine playerStateMachine , int attackID) : base(playerStateMachine)
    {
        attack = playerStateMachine.Attacks[attackID];
    }
    Attack  attack;
    public override void Enter()
    {
        playerStateMachine.animator.CrossFadeInFixedTime(attack.AnimationName, 0.1f);
        playerStateMachine.PlayTrail();
    }
    public override void Update(float deltatime)
    {
       
    }
    public override void Exit()
    {

    }
}
