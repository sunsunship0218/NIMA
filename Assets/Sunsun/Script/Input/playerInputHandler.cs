using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.EventSystems;

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
    public event Action<PlayerInput> onControlsChanged;

    //狀態變數
    public bool isOnLockon;
    public bool isAttacking;
    public bool isDashing { get; private set; }
    public bool isUsingPad { get; private set; }
    //格檔跟防禦的相關變數
    public bool isBlocking;
    public bool isParrying;

    PlayerControllers playercontrollers;
    PlayerInput playerInput;
    //持續按住不攻擊
   const float holdTimeThreshold = 0.4f; 
    private float holdTimer = 0f;
    private bool isButtonHeld = false;
    //DASH的冷卻參數
    public float DodgeCooldown = 1f; // 冷卻秒數
    private float lastDodgeTime = Mathf.NegativeInfinity; // 記錄上次閃避的時間
    [SerializeField] PlayerStateMachine playerStateMachine;
    void Awake()
    {
        playercontrollers = new PlayerControllers();
        playerInput=GetComponent<PlayerInput>();
 
    }
  
    void Start()
    {

        //playerInputHandler回傳callbacks
        playercontrollers.Player.SetCallbacks(this);
        playercontrollers.Player.Enable();
        playercontrollers.UI.Enable();

    }
    private void Update()
    {
        //按下按鈕
        if (isButtonHeld)
        {
            // 持續按住按鈕更新時間
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTimeThreshold)
            {
                // 持續按住超過時間,不攻擊
                isAttacking = false;
                isButtonHeld = false; 
           
            }
         
        }
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
        //點擊UI時,不攻擊
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //短按攻擊
        if (context.performed && context.interaction is PressInteraction)
        {
            isButtonHeld = true;
            isAttacking = true;
            holdTimer = 0f;

        }
        //長按不攻擊
        else if (context.performed && context.interaction is HoldInteraction)
        {
          
            isAttacking = false;
           
        }
        else if (context.canceled)
        {
            isAttacking = false;
            isButtonHeld = false;
        }
     
    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        // 冷卻中，不執行 dodge
        if (Time.time < lastDodgeTime + DodgeCooldown)
        {

            return; 
        }
        // 1. 還在動畫中 ,禁止再次 Dodge
        if (playerStateMachine.IsDodgeAnimationPlaying())
        {
            return;
        }


        lastDodgeTime = Time.time;
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
     
    }

    private void OnControllerChange(PlayerInput input)
    {
        if(input.currentControlScheme != "Gamepad")
        {
            isUsingPad = false;
        }
        isUsingPad = true;
    }
    public bool GetIsAttacking()
    {
        return isAttacking;
    }
    void OnEnable()
    {
        if (playerInput != null)
        {
            playerInput.onControlsChanged += OnControllerChange;
        }
    }
    void OnDisable()
    {
        playerInput.onControlsChanged -= OnControllerChange;
    }
}



