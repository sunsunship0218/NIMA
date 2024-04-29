using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseState : State
{
    public attackState attackState;
    public bool isInAttackRange;
    public override State RunCurrentState()
    {
        if (isInAttackRange)
        {
            return attackState;
        }
        else
        {
            return this;
        }
        
    }
}
