using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    float Move_speed;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    Collider collider;
    [SerializeField]
    CharacterController characterController;
    PlayerControllers playerControllers_;
    Keyboard keyboard;
    myDirection MyDirection;
    float degree = 0;

    //��ť vactor value
    public Vector2 inputValue;
    public bool IsJump  =false;

    private void Awake()
    {     
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        playerControllers_ = new PlayerControllers();     
    }
    void Start()
    {
        //��������
        playerControllers_.PlayerMove.AD_Move.performed += value => inputValue = value.ReadValue<Vector2>();  
        //��������
        playerControllers_.PlayerMove.AD_Move.canceled += _ => inputValue = Vector2.zero;
       
    }
    void Update()
    {
       
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            IsJump = true;
        }

        // ���ʨ쥿�T��position
        float moveFactor = Move_speed * Time.deltaTime * 10f;
        MoveCharacter(moveFactor);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, degree, 0), 8 * Time.deltaTime);       
    }
     void FixedUpdate()
     {
        Move();    
     }
    //���ʪ��a��m
    private void MoveCharacter(float moveFactor)
    {
        //Here is bug
        //���������J��
        float Horizontal = Input.GetAxis("Horizontal");
        // inputValue.x; 

        float Gravity =15f;
        float JumpHeight =0f;
        //���N�y���k0�A���B�z
        Vector3 trans = Vector3.zero;
 
        if (MyDirection == myDirection.Front)
        {
            trans = new Vector3(-Horizontal * moveFactor, -Gravity * moveFactor, 0f);
           
        }
        else if (MyDirection == myDirection.Right)
        {
            trans = new Vector3(0f, -Gravity * moveFactor, Horizontal * moveFactor);
        }
        else if (MyDirection == myDirection.Back)
        {
            trans = new Vector3(Horizontal * moveFactor, -Gravity * moveFactor, 0f);
        }
        else if (MyDirection == myDirection.Left)
        {
            trans = new Vector3(0f, -Gravity * moveFactor, -Horizontal * moveFactor);
        }
        if (IsJump)
        {
            transform.Translate(Vector3.up * JumpHeight * Time.deltaTime);
        }

        characterController.SimpleMove(trans);
       // Debug.Log("Position"+trans);
    }

    void Move()
    {
        //Move jump run different speed
        //set move speed
        rb.velocity = new Vector3(inputValue.x * Move_speed, rb.velocity.y, inputValue.y * Move_speed);      
    }

    public void Upd_myFacingDirection(myDirection newDirection, float angle)
    {
        MyDirection = newDirection;
        degree = angle;
        Debug.Log(angle);
    }
    

    public myDirection CmdFacingDirection
    {

        set
        {
            MyDirection = value;
        }

    }
    void OnEnable()
    {
        playerControllers_.PlayerMove.Enable();
    }
    void OnDisable()
    {
        playerControllers_.PlayerMove.Disable();
    }
}
