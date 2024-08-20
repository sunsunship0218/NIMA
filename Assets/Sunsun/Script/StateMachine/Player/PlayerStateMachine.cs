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

    //移動相關的參數
    [field: SerializeField]
    public float freeLookMoveSpeed{ get; private set; }
    [field: SerializeField]
    public float LockonMoveSpeed { get; private set; }
    [field: SerializeField]
    public ForceReceiver forceReceiver { get; private set; }
    [field: SerializeField]
    public float moveRotationDamping { get; private set; }

    //攻擊combo
    [field: SerializeField]
    public Attack[ ] Attacks { get; private set; }
    //其他componment
    [field: SerializeField]
    public Animator animator { get; private set; }

    [field: SerializeField]
    public Targeter targeter { get; private set; }


    [SerializeField] TrailRenderer trailRenderer;

    public Transform mainCameraTransform { get; private set; }
    void Start()
     {
        mainCameraTransform = Camera.main.transform;
        //this 就是現在的PlayerStateMachine實例
        SwitchState(new PlayerFreeLookState(this)); 
     }

    // Test play effect
/*
public void PlayTrail()
{
    if (trailRenderer != null && playerInputHandler.isAttacking)
    {
        trailRenderer.emitting = true;
    }
}
public void StopTrail()
{
    if (trailRenderer != null)
    {
        trailRenderer.emitting = false;
    }
}
*/
}
