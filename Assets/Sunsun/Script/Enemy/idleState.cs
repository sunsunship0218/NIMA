using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idelState : State
{
    public bool isSeePlayer;
    public chaseState chaseState;
    public override State RunCurrentState()
    {
        if (isSeePlayer)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
       
    }
}
