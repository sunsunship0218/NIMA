using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//接收實際的狀態機的實作
public class PlayerStateMachine : StateMachine
{
    [field: SerializeField]
    public playerInputHandler playerInputHandler { get; private set; }
     void Start()
     {
        //this 就是現在的PlayerStateMachine實例
        SwitchState(new PlayerTestState(this)); 
     }

 

}
