using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFX;
public class PlayerDashingState : PlayerBaseState
{
    Vector2 dodgingDirectionInput;
    Vector3 dodgeDirection;

    public PlayerDashingState(PlayerStateMachine playerStateMachine, Vector2 dodgingDirectionInput) : base(playerStateMachine)
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }
    //animator paramater
    readonly int DodgeBlendtreeHASH = Animator.StringToHash("DodgeBlendtree");
    readonly int DodgeForwardHASH = Animator.StringToHash("DashingForward");
    readonly int DodgeRightHASH = Animator.StringToHash("DashingRight");
    const float animatorDampSpeed = 0.14f;
    const float crossfadeDuration = 0.1f;
    //
    float DodgingDuration;
    public override void Enter()
    {

        //獲取玩家輸入方向
        dodgingDirectionInput = playerStateMachine.playerInputHandler.movementValue.normalized;

        if (dodgingDirectionInput == Vector2.zero)
        {
            dodgingDirectionInput = new Vector2(0, 1); // 向前
        }

        //躲避方向
        dodgeDirection = playerStateMachine.transform.forward.normalized;
            /*(playerStateMachine.transform.forward * dodgingDirectionInput.y + 
                                            playerStateMachine.transform.right * dodgingDirectionInput.x).normalized;*/
        //躲避時間
        DodgingDuration = playerStateMachine.DodgeTime;
        
        //動畫撥放
        playerStateMachine.animator.SetFloat(DodgeForwardHASH, dodgingDirectionInput.y);
        playerStateMachine.animator.SetFloat(DodgeRightHASH, dodgingDirectionInput.x);
        playerStateMachine.animator.CrossFadeInFixedTime(DodgeBlendtreeHASH, crossfadeDuration);
        //無敵時間
        playerStateMachine.playerHealth.healthSystem.SetInvunerable(true);
       // Debug.Log(dodgingDirectionInput);
    }
    public override void Update(float deltatime)
    {
        MeshTrail meshTrail = playerStateMachine.GetComponentInChildren<VFX.MeshTrail>();
        if (meshTrail != null)
        {
            meshTrail.playGhostTrail();
        }
        Facetarget();
        //計算躲避的位移
        float dodgeSpeed = playerStateMachine.DodgeDistance / playerStateMachine.DodgeTime;
        Vector3 movement  = dodgeSpeed * dodgeDirection *deltatime;
        // movement += playerStateMachine.transform.right * dodgingDirectionInput.x * dodgeSpeed;
        //     movement += playerStateMachine.transform.forward * dodgingDirectionInput.y * dodgeSpeed;
        Move(movement);
        //減少時長
        DodgingDuration -= deltatime;
        //獲取動畫撥放狀態,防止沒有撥放完就切換狀態
        AnimatorStateInfo currentStateInfo = playerStateMachine.animator.GetCurrentAnimatorStateInfo(0);
        bool AnimationFinished = (currentStateInfo.IsName("DodgeBlendtree") && currentStateInfo.normalizedTime >=0.8f);
        if (DodgingDuration <= 0f && AnimationFinished)
        {
            Debug.Log(AnimationFinished);
            if (playerStateMachine.targeter.currentTarget == null)
            {
                playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
            }
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));

        }
      

    }
    public override void Exit()
    {
        playerStateMachine.playerHealth.healthSystem.SetInvunerable(false);


    }
}



