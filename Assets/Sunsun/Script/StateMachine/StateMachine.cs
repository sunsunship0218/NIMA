using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class StateMachine : MonoBehaviour
{
    State currentState;

    void Update()
    {
        // ?= �p�G�e�̬Onull,�᭱���p������,�Ϥ�
        currentState?.Update(Time.deltaTime);
    }

    void SwitchState(State nextState)
    {
        //if current state != null
        currentState?.Exit();
        currentState = nextState;
        //�i�JnextState
        currentState.Enter();
    }
}
