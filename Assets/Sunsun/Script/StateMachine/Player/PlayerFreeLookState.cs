using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//實際運作的狀態,ex 戰鬥狀態...
//真正實作狀態機功能
//決定實作的狀態機功能切換到其他 狀態機
public class PlayerFreeLookState : PlayerBaseState
{
    //建構式
    //base傳遞參數playerStateMachine給繼承的建構式
    public PlayerFreeLookState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    //animator paramater
    readonly int unLockOnBlendtreeHASH = Animator.StringToHash("FreeLookBlendtree");
    readonly int FREELOOKSPEEDHASH = Animator.StringToHash("FreeLookSpeed");
    const float animatorDampSpeed = 0.14f;
    const float crossfadeDuration = 0.1f;
    float speed;
    public override void Enter()
    {
        Debug.Log("Enter freeLook");
        playerStateMachine.playerInputHandler.isOnLockon = false;
      playerStateMachine.playerInputHandler.isAttacking = false;
        playerStateMachine.animator.CrossFadeInFixedTime(unLockOnBlendtreeHASH, crossfadeDuration);

        //按下鎖定後切換狀態  
        playerStateMachine.playerInputHandler.targetEvent += OnTarget;
        //按下躲避後切換狀態
        playerStateMachine.playerInputHandler.dodgeEvent += Ondodge;

    }
    public override void Update(float deltatime)
    {
        //按下攻擊,進入攻擊狀態
        if (playerStateMachine.playerInputHandler.isAttacking)
        {
            Debug.Log("Press Attack");
            Debug.Log("Is Attacking?" + playerStateMachine.playerInputHandler.isAttacking);
            playerStateMachine.SwitchState(new PlayerAttackingState(playerStateMachine, 0));
            return;
        }
        //按下防禦,進入防禦
        if (playerStateMachine.playerInputHandler.isBlocking )
        {
            Debug.Log("Press Defend");
            playerStateMachine.SwitchState(new PlayerBlockingState(playerStateMachine));
            return;
        }
        //按下格擋,在格擋範圍],進入格擋
        if (playerStateMachine.playerInputHandler.isParrying)
        {
            playerStateMachine.SwitchState(new PlayerParringState(playerStateMachine));
            return;
        }
        //計算移動距離
        Vector3 movementVec3 = calculateMovement(deltatime);
        Move(movementVec3 *  playerStateMachine.freeLookMoveSpeed,deltatime);
        //根據輸入播放對應的動畫
        speed = Mathf.SmoothStep(speed, playerStateMachine.playerInputHandler.movementValue.magnitude, 0.1f);
        playerStateMachine.animator.SetFloat(FREELOOKSPEEDHASH, speed, animatorDampSpeed, deltatime);

    
        FaceMovementDirection(movementVec3,deltatime);

      

    }
    public override void Exit()
    {
        playerStateMachine.playerInputHandler.targetEvent -= OnTarget;
        
    }

    void OnTarget()
    {
        if (!playerStateMachine.targeter.SelectTarget()) { return; }     
        if (!playerStateMachine.playerInputHandler.isOnLockon)
        {
            playerStateMachine.playerInputHandler.isOnLockon = true;
            playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
          
        }
       
    }

    void Ondodge()
    {
 
        playerStateMachine.SwitchState(new PlayerDashingState(playerStateMachine, playerStateMachine.playerInputHandler.movementValue));
    }

    void FaceMovementDirection(Vector3 movementVec3,float deltatime)
    {
        if (movementVec3.sqrMagnitude > 0.0001f)
        {
            playerStateMachine.transform.rotation = Quaternion.Lerp(
            playerStateMachine.transform.rotation,// current transform
            Quaternion.LookRotation(movementVec3),//rotated target's tranform
           deltatime * playerStateMachine.moveRotationDamping
            );
        }
           
    }

    Vector3 calculateMovement(float deltatime )
    {
      
        Vector3 forward, right ;
        forward = playerStateMachine.mainCameraTransform.forward;
        right = playerStateMachine.mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

       forward.Normalize();
       right.Normalize();

        // 移動的距離*重力(movementValue.y)
        return  forward*playerStateMachine.playerInputHandler.movementValue.y+
                     right*playerStateMachine.playerInputHandler.movementValue.x;       
    }
    


}
