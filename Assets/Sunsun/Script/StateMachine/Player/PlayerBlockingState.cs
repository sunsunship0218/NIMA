using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState :PlayerBaseState
{
    readonly int BlockHASH = Animator.StringToHash("Block");
    const float duration = 0.14f;
    float minBlockTime = 0.2f;
    float timer = 0f;

    public PlayerBlockingState (PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    public override void Enter()
    {
        Debug.Log($"Enter Block: {Time.time}");
        timer = 0f;
        playerStateMachine.playerHealth.healthSystem.SetInvunerable(true);
        playerStateMachine.animator.CrossFadeInFixedTime(BlockHASH, duration);
   
    }
    public override void Update(float deltaTime)
    {
        playerStateMachine.blockPostureHandler.SetPosture(5f *2);
        MoveWithDeltatime(deltaTime);
        //緩衝時間
        timer += deltaTime;
        //獲取動畫撥放狀態,防止沒有撥放完就切換狀態
        AnimatorStateInfo currentStateInfo = playerStateMachine.animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName("Block") && currentStateInfo.normalizedTime < 0.7f)
        {
            return;  
        }
        //阻止狀態重複切換
        if (timer < minBlockTime)
        {
            return;
        }
   
        if (!playerStateMachine.playerInputHandler.isBlocking)
        {
            playerStateMachine.SwitchState(new PlayerTargetingState(playerStateMachine));
            return;
        }
        //如果設置了這個條件,在沒有目標的時候,動畫會不同撥放
       /* if (playerStateMachine.targeter.currentTarget == null)
        {
            playerStateMachine.SwitchState(new PlayerFreeLookState(playerStateMachine));
            return;
        }*/
    }
    public override void Exit()
    {
        Debug.Log($"Exit Block: {Time.time}");
        playerStateMachine.playerHealth.healthSystem.SetInvunerable(false);
    }
}
