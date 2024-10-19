using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    //animator parameter
    readonly int LockOnBlendtreeHASH = Animator.StringToHash("LockOnBlendtree");
    // animator paramators
    readonly int LockOnForwardHASH = Animator.StringToHash("LockonForward");
    readonly int LockonRightHASH = Animator.StringToHash("LockonRight");
    const float animatorDampSpeed = 0.1f;
    public override void Enter()
    {
        playerStateMachine.playerInputHandler.isOnLockon = true;
        playerStateMachine.animator.Play(LockOnBlendtreeHASH);

        //按下鎖定後切換狀態  
        playerStateMachine.playerInputHandler.cancelTargetEvent += OnCancleTarget;
        //按下躲避後切換狀態
        playerStateMachine.playerInputHandler.dodgeEvent += Ondodge;
    }
    public override void Update(float deltatime)
    {
        //攻擊的話要切換狀態
        if (playerStateMachine.playerInputHandler.isAttacking)
        {
            //0是Attack陣列的combo
            playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, 0));
            return;
        }
        //防禦的話切換狀態
        if (playerStateMachine.playerInputHandler.isBlocking)
        {
            playerStateMachine.SwitchState(new PlayerBlockingState(playerStateMachine));
            return;
        }
        //現在沒有鎖定目標的話,回到freelook
        if (playerStateMachine.targeter.currentTarget == null)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
            return;
        }
        Vector3 movement = CalculateMovement(deltatime);
        Move(movement * playerStateMachine.LockonMoveSpeed, deltatime);
        //更新動畫
        UpdAnimator(deltatime);
        //朝向目標
        Facetarget();
   
    }
    public override void Exit()
    {
        playerStateMachine.playerInputHandler.cancelTargetEvent -= OnCancleTarget;

    }

   void  UpdAnimator(float deltatime)
    {

        // x,y不能用switch case判斷,移動的float值不會剛好符合條件
        if (playerStateMachine.playerInputHandler.movementValue.y == 0)
        {
            playerStateMachine.animator.SetFloat(LockOnForwardHASH, 0,animatorDampSpeed,deltatime);
        }
        else
        {
            float value = playerStateMachine.playerInputHandler.movementValue.y > 0 ? 1f : -1f;
            playerStateMachine.animator.SetFloat(LockOnForwardHASH, value, animatorDampSpeed, deltatime);
        }

        if (playerStateMachine.playerInputHandler.movementValue.x == 0)
        {
            playerStateMachine.animator.SetFloat(LockonRightHASH, 0, animatorDampSpeed, deltatime);
        
        }
        else
        {
            float value = playerStateMachine.playerInputHandler.movementValue.x > 0 ? 1f : -1f;
            playerStateMachine.animator.SetFloat(LockonRightHASH, value, animatorDampSpeed, deltatime);
        }


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

    void Ondodge()
    {
        Debug.Log("DODGE");
        playerStateMachine.SwitchState(new PlayerDashingState(playerStateMachine, playerStateMachine.playerInputHandler.movementValue));
    }

    //計算移動距離
    Vector3 CalculateMovement(float deltatime)
      {
        Vector3 movement = new Vector3();
      
        
            movement += playerStateMachine.transform.right * playerStateMachine.playerInputHandler.movementValue.x;
            movement += playerStateMachine.transform.forward * playerStateMachine.playerInputHandler.movementValue.y;
        

        return movement;
      }

}
