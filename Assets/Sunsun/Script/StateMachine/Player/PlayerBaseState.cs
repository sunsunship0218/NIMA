using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//將實作完的狀態機功能交給負責過度的模組
//在建構式決定是誰要接收State模組的實作
public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine playerStateMachine;
    public PlayerBaseState(PlayerStateMachine playerStateMachine) 
    { 
        this.playerStateMachine = playerStateMachine;
    }
    protected void Move(Vector3 motion, float deltatime)
    {
        playerStateMachine.characterController.Move((motion+playerStateMachine.forceReceiver.movement)*deltatime);
    }
    protected void Facetarget()
    {
        if (playerStateMachine.targeter.currentTarget == null){ return; }
       Vector3 faceTargetPos;
        faceTargetPos = playerStateMachine.targeter.currentTarget.transform.position - playerStateMachine.transform.position;
        faceTargetPos.y = 0f;
        playerStateMachine.transform.rotation = Quaternion.LookRotation(faceTargetPos);
    }
   
    protected void MoveWithDeltatime(float deltatime)
    {
        Move(Vector3.zero,deltatime);
    }
}
