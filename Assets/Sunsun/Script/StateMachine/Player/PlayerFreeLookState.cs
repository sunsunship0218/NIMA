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

    public override void Enter()
    {
        playerStateMachine.StopTrail();
        playerStateMachine.playerInputHandler.isOnLockon = false;
        playerStateMachine.animator.Play(unLockOnBlendtreeHASH);

        Debug.Log("Entered Free look state" + Time.deltaTime);
        //按下鎖定後切換狀態  
        playerStateMachine.playerInputHandler.targetEvent += OnTarget;

    }
    public override void Update(float deltatime)
    {
        Vector3 movementVec3 = calculateMovement();
        Move(movementVec3 *  playerStateMachine.freeLookMoveSpeed,deltatime);
        if (playerStateMachine.playerInputHandler.movementValue == Vector2.zero)
        {
            playerStateMachine.animator.SetFloat(FREELOOKSPEEDHASH, 0, animatorDampSpeed, deltatime);
            return;
        }
        playerStateMachine.animator.SetFloat(FREELOOKSPEEDHASH, 1, animatorDampSpeed, deltatime);
        FaceMovementDirection(movementVec3,deltatime);

        //if (playerStateMachine.playerInputHandler.isOnLockon)
        //    OnTarget();
      
    }
    public override void Exit()
    {
        playerStateMachine.playerInputHandler.targetEvent -= OnTarget;
        Debug.Log("Free look exist" +Time.deltaTime);
    }

    void OnTarget()
    {
        if (!playerStateMachine.targeter.SelectTarget()) { return; }     
        if (!playerStateMachine.playerInputHandler.isOnLockon)
        {
            playerStateMachine.playerInputHandler.isOnLockon = true;
            playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
            Debug.Log("OnTarget " + Time.deltaTime);
        }
       
    }
    

    void FaceMovementDirection(Vector3 movementVec3,float deltatime)
    {
        playerStateMachine.transform.rotation = Quaternion.Lerp(
             playerStateMachine.transform.rotation,// current transform
             Quaternion.LookRotation(movementVec3),//rotated target's tranform
            deltatime * playerStateMachine.moveRotationDamping
             ) ;
    }

    Vector3 calculateMovement( )
    {
        Vector3 forward, right ;
        forward = playerStateMachine.mainCameraTransform.forward;
        right = playerStateMachine.mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

       forward.Normalize();
       right.Normalize();

        return  forward*playerStateMachine.playerInputHandler.movementValue.y+
                     right*playerStateMachine.playerInputHandler.movementValue.x;       
    }
    


}
