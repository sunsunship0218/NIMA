using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    public abstract void Enter();
    //if in the state, update every frame
    public abstract void Update(float deltaTime);
    public abstract void Exit();
}