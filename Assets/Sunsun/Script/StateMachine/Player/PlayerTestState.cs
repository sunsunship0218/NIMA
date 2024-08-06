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

    //time
    float Timer;

    public override void Enter()
    {
        //Jump事件訂閱OnJump方法,Jump的實作寫在OnJump
        playerStateMachine.playerInputHandler.jumpEvent +=OnJump;

    }
    public override void Update(float deltatime)
    {
        Timer+= deltatime;
        Debug.Log( Timer);

    }
    public override void Exit()
    {
        playerStateMachine.playerInputHandler.jumpEvent -= OnJump;
        Debug.Log("Exit, exist time :  " + Timer);
    }

    public   void OnJump()
    {
        playerStateMachine.SwitchState(new PlayerTestState(playerStateMachine));
    }

}
