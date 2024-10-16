using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


//將輸入要處理的邏輯綁訂到回調
public class playerInputHandler : MonoBehaviour, PlayerControllers.IPlayerActions
{
    // 移動ㄉ值
    public Vector2 movementValue { get; private set; }
    //Action map Action event
    public event Action jumpEvent;
    public event Action dodgeEvent;
    public event Action targetEvent;
    public event Action cancelTargetEvent;

    //狀態變數
    public bool isOnLockon;
    public bool isAttacking { get; private set; }
    public bool isDashing { get; private set; }
    public bool isBlocking;

    PlayerControllers playercontrollers;
    void Awake()
    {
        playercontrollers = new PlayerControllers();
     
    }
    void Start()
    {

        //playerInputHandler回傳callbacks
        playercontrollers.Player.SetCallbacks(this);
        playercontrollers.Player.Enable();
    }
     void OnDestroy()
     {
        playercontrollers.Player.Disable();
     }

    //IPlayerActions的介面規範
    public void  OnMove(InputAction.CallbackContext context)
    {
       movementValue= context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        jumpEvent?.Invoke();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
      
        if (context.performed)
        {
            isAttacking = true;
        }
        else  if(context.canceled)
        {
            isAttacking= false;
        }
    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        dodgeEvent?.Invoke();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        
    }
    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (isOnLockon)
        {
            cancelTargetEvent?.Invoke();
       
        }
        else
        {
            targetEvent?.Invoke();
        }
        isOnLockon=!isOnLockon;
    }
    public void OnParry(InputAction.CallbackContext context)
    {

    }
    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBlocking = true;
        }
        else if (context.canceled)
        {
            isBlocking = false;
        }
    }
}


