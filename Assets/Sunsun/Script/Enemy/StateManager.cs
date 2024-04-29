using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
   public State currentState;
    void Update()
    {
        
    }
    void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();
        if (nextState != null)
        {
            //switch to next state
            SwitchToNextState(nextState);

        }
    }
    void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }
}
