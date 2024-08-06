using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//實際運作的狀態,ex 戰鬥狀態...
//真正實作狀態機功能
//決定實作的狀態機功能切換到其他 狀態機
public class PlayerTestState : PlayerBaseState
{
    //建構式
    //base傳遞參數playerStateMachine給繼承的建構式
    public PlayerTestState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
       

    }
    public override void Update(float deltatime)
    {
        Vector3 movementVec3 = new Vector3();
        movementVec3.x = playerStateMachine.playerInputHandler.movementValue.x;
        movementVec3.y = 0;
        movementVec3.z = playerStateMachine.playerInputHandler.movementValue.y;
        playerStateMachine.characterController.Move(movementVec3 * deltatime * playerStateMachine.freeLookMoveSpeed);

        if (playerStateMachine.playerInputHandler.movementValue == Vector2.zero)
        {
            playerStateMachine.animator.SetFloat("FreeLookSpeed", 0, 0.14f,deltatime);
            return;
        }
        playerStateMachine.animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltatime);
        playerStateMachine.transform.rotation = Quaternion.LookRotation(movementVec3);


    }
    public override void Exit()
    {
      
    }



}
