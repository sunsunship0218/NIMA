using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{
    Vector3 Dashingdirection;
    public PlayerDashingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) 
    { 

    }
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
