using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem.Interactions;


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
    //格檔跟防禦的相關變數
    public bool isBlocking;
    public bool isParrying;

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
    public void OnBlockAndParry(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (context.interaction is TapInteraction)
            {
                // 短按格擋
                isParrying = true;
                isBlocking = false;
                StartCoroutine(ResetParry());
            }
            else if (context.interaction is HoldInteraction)
            {
                // 長按防禦
                isParrying = false;
                isBlocking = true;
              
            }
        }

        else if (context.canceled)
        {
            
            isBlocking = false;
            isParrying = false;
        }
    }
    private IEnumerator ResetParry()
    {
        // 等待一個幀後重置 isParrying
        yield return null;
        isParrying = false;
        Debug.Log("Parry reset at: " + Time.time);
    }
}



