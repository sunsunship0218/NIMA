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
    protected void Move(Vector3 motion)
    {
        playerStateMachine.characterController.Move(motion + playerStateMachine.forceReceiver.movement);
    }
    protected void MoveWithDeltatime(float deltatime)
    {
        Move(Vector3.zero, deltatime);
    }
    protected void Facetarget()
    {
        if (playerStateMachine.targeter.currentTarget == null)
        {
                playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
                return;
        }
       Vector3 faceTargetPos;
        faceTargetPos = playerStateMachine.targeter.currentTarget.transform.position - playerStateMachine.transform.position;
        faceTargetPos.y = 0f;
        playerStateMachine.transform.rotation = Quaternion.LookRotation(faceTargetPos);
    }
  public void FaceEnemy()
    {
        if (playerStateMachine. EnemyList.Count == 0 || playerStateMachine.EnemyList == null)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
            return;

        }
            
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        //找出最近的敵人
        foreach (var enemy in playerStateMachine.EnemyList)
        {
            float DistanceToEnemy = Vector3.Distance(playerStateMachine.transform.position, enemy.transform.position);
            if (DistanceToEnemy < nearestDistance)
            {
                nearestDistance = DistanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //面向敵人
        if (nearestEnemy != null)
        {
            Vector3 faceTargetPos = nearestEnemy.transform.position - playerStateMachine.transform.position;
            faceTargetPos.y = 0f; // 忽略y軸，讓旋轉只影響水平面
            playerStateMachine.transform.rotation = Quaternion.LookRotation(faceTargetPos);
        }



    }
    protected void ReturntoLocomotion()
    {
        // 有目標的話,繼續鎖定
        if(playerStateMachine.targeter.currentTarget != null)
        {
            playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
        }
        else
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
        }
    }
}
