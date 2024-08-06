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

}
