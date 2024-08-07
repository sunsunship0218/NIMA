using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    public override void Enter()
    {
        playerStateMachine.playerInputHandler.isTargetting = true;
        //取消後觸發狀態
        playerStateMachine.playerInputHandler.cancleTargetEvent += OnCancleTarget;     
        Debug.Log("enter targeting state");
    }
    public override void Update(float deltatime)
    {

    }
    public override void Exit()
    {
        playerStateMachine.playerInputHandler.cancleTargetEvent -= OnCancleTarget;
        Debug.Log("Exited Targeting State");
    }

    void OnCancleTarget()
    {
              playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        Debug.Log("cancletarget");
    }
}
