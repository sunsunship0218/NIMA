using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//接收實際的狀態機的實作
public class PlayerStateMachine : StateMachine
{
    //接收玩家輸入傳送至Mono的窗口
    [field: SerializeField]
    public playerInputHandler playerInputHandler { get; private set; }

    [field: SerializeField]
    public CharacterController characterController { get; private set; }

    [field: SerializeField]
    public float freeLookMoveSpeed{ get; private set; }

    [field: SerializeField]
    public Animator animator { get; private set; }

    void Start()
     {
        //this 就是現在的PlayerStateMachine實例
        SwitchState(new PlayerTestState(this)); 
     }

 

}
