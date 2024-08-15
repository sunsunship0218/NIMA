using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    //animator parameter
    readonly int LockOnBlendtreeHASH = Animator.StringToHash("LockOnBlendtree");
    public override void Enter()
    {
        playerStateMachine.playerInputHandler.isOnLockon = true;
        playerStateMachine.animator.Play(LockOnBlendtreeHASH);

        Debug.Log("Entered Targeting State" + Time.deltaTime);
        //按下鎖定後切換狀態  
        playerStateMachine.playerInputHandler.cancelTargetEvent += OnCancleTarget;
    }
    public override void Update(float deltatime)
    {
        //現在沒有鎖定目標的話
        if (playerStateMachine.targeter.currentTarget == null)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
            return;
        }
        Vector3 movement = CalculateMovement();
        Move(movement * playerStateMachine.LockonMoveSpeed, deltatime);
        Facetarget();
   
    }
    public override void Exit()
    {
        playerStateMachine.playerInputHandler.cancelTargetEvent -= OnCancleTarget;
        Debug.Log("Exited Targeting State" +Time.deltaTime);
    }

    void OnCancleTarget()
    {
        playerStateMachine.targeter.CancleLockon();
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
