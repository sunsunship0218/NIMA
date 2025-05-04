using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    float duration;
    public override void Enter()
    {
      //  Debug.Log("Player dead enter");
        playerStateMachine.ragDoll.toogleRagdoll(true);
        playerStateMachine.RightweaponDamage.gameObject.SetActive(false);
        playerStateMachine.LeftweaponDamage.gameObject.SetActive(false);
   
    }
    public override void Update(float deltaTime)
    {
       
    }
    public override void Exit()
    {

    }
}
