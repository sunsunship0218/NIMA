using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class StateMachine : MonoBehaviour
{
    State currentState;

    void Update()
    {
        // ?= 如果前者是null,後面狀況不執行,反之
        currentState?.Update(Time.deltaTime);
    }

    void SwitchState(State nextState)
    {
        //if current state != null
        currentState?.Exit();
        currentState = nextState;
        //進入nextState
        currentState.Enter();
    }
}
