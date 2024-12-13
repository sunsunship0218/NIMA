using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParringState : PlayerBaseState
{
    readonly int ParryHash = Animator.StringToHash("Parry");
    const float duration = 0.14f;
    float minBlockTime = 0.2f;
    float timer = 0f;

    public PlayerParringState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    public override void Enter()
    {
        timer = 0f;
        playerStateMachine.playerHealth.healthSystem.SetInvunerable(true);
        playerStateMachine.animator.CrossFadeInFixedTime(ParryHash, duration);

    }
    public override void Update(float deltaTime)
    {
        MoveWithDeltatime(deltaTime);
        //緩衝時間
        timer += deltaTime;
        //獲取動畫撥放狀態,防止沒有撥放完就切換狀態
        AnimatorStateInfo currentStateInfo = playerStateMachine.animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName("Parry") && currentStateInfo.normalizedTime < 0.8f)
        {
            return;
        }
        //阻止狀態重複切換
        if (timer < minBlockTime)
        {
            return;
        }

        if (!playerStateMachine.playerInputHandler.isParrying)
        {
            playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
            return;
        }
        Debug.Log(playerStateMachine.playerInputHandler.isParrying);
        //如果設置了這個條件,在沒有目標的時候,動畫會不同撥放
        /* if (playerStateMachine.targeter.currentTarget == null)
         {
             playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
             return;
         }*/
    }
    public override void Exit()
    {

            playerStateMachine.playerHealth.healthSystem.SetInvunerable(false);
    }
}
