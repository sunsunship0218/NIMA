using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//只負責管理狀態"轉換",不管理切換到什麼狀態
public  abstract class StateMachine : MonoBehaviour
{
    State currentState;

   //在每frame調用狀態機的update,更新現在的前端畫面
    void Update()
    {
        // ?= 如果前者是null,後面狀況不執行,反之
        currentState?.Update(Time.deltaTime);
            
    }

    public void SwitchState(State nextState)
    {
        State oldState = currentState;
        //if current state != null
        currentState?.Exit();
        currentState = nextState;
        //進入nextState
        currentState.Enter();
        Debug.Log("Switching from " + oldState?.GetType().Name + " to " + nextState.GetType().Name);

    }
}
