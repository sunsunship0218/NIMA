using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//定義所有狀態的概念
//具體實作時只寫各狀態機要做的事,不負責狀態過渡
public abstract class State
{
    public abstract void Enter();
    //if in the state, update every frame
    public abstract void Update(float deltaTime);
    public abstract void Exit();
}