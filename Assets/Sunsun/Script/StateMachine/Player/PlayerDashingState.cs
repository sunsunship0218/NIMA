using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{
    Vector3 Dashingdirection;
    public PlayerDashingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    //animator paramater
    readonly int DodgeBlendtreeHASH = Animator.StringToHash("DodgeBlendtree");
    readonly int DodgeSPEEDHASH = Animator.StringToHash("DodgeSpeed");
    const float animatorDampSpeed = 0.14f;
    const float crossfadeDuration = 0.1f;
    public override void Enter()
    {
        playerStateMachine.playerInputHandler.isOnLockon = false;
    }
    public override void Update(float deltatime)
    {
        playerStateMachine.characterController.Move(Dashingdirection * deltatime);
    }
    public override void Exit()
    {

    }

    IEnumerator DashCoroutine()
    {
        yield return null;
    }
}
