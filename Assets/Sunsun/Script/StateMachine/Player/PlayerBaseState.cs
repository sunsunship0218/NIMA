using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerBaseState : State
{
   protected PlayerStateMachine playerStateMachine;
    public PlayerBaseState(PlayerStateMachine playerStateMachine) 
    { 
        this.playerStateMachine = playerStateMachine;
    }

}
