using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


//將輸入要處理的邏輯綁訂到回調
public class playerInputHandler : MonoBehaviour, PlayerControllers.IPlayerActions
{
    //Action map Action event
    public event Action jumpEvent;
    public event Action dodgeEvent;

    PlayerControllers playercontrollers;
    void Start()
    {
        playercontrollers= new PlayerControllers();
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

    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        dodgeEvent?.Invoke();
    }


}


/*
 *   [SerializeField]
    float Move_speed;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Collider collider;

    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    Animator animator;

    PlayerControllers playerControllers_;

    //監聽 vactor value
    public Vector2 inputValue;
    public bool IsJump = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        playerControllers_ = new PlayerControllers();
    }
    void Start()
    {
        //水平移動
        playerControllers_.Player.Move.performed += value => inputValue = value.ReadValue<Vector2>();
        //垂直移動
        playerControllers_.Player.Move.canceled += _ => inputValue = Vector2.zero;

    }
    void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            IsJump = true;
        }

    }
    void FixedUpdate()
    {
        Move();
    }



    void Move()
    {
        //Move jump run different speed
        //set move speed
        rb.velocity = new Vector3(inputValue.x * Move_speed, rb.velocity.y, inputValue.y * Move_speed);
    }


    void OnEnable()
    {
        playerControllers_.Player.Enable();
    }
    void OnDisable()
    {
        playerControllers_.Player.Disable();
    }
 */
