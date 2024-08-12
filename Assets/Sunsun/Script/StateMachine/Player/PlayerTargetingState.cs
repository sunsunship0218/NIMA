using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    public override void Enter()
    {
   playerStateMachine.playerInputHandler.isOnLockon = true;
        Debug.Log("Entered Targeting State" + Time.deltaTime);
        //按下鎖定後切換狀態  
        playerStateMachine.playerInputHandler.cancelTargetEvent += OnCancleTarget;
    }
    public override void Update(float deltatime)
    {
   
    }
    public override void Exit()
    {
        playerStateMachine.playerInputHandler.cancelTargetEvent -= OnCancleTarget;
        Debug.Log("Exited Targeting State" +Time.deltaTime);
    }

    void OnCancleTarget()
    {
      
        if (playerStateMachine.playerInputHandler.isOnLockon)
        {
            playerStateMachine.playerInputHandler.isOnLockon = false;
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
            Debug.Log("cancletarget: " + Time.deltaTime);
        }
        else
        {
            return;
        }
    }
        

}
