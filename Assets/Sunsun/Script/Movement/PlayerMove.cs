using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NIMA.Camera.Rotate;


namespace NIMA.Movement
{
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

        [SerializeField]
        Animator animator;


        PlayerControllers playerControllers_;
        Keyboard keyboard;
        myDirection MyDirection;
        float degree = 0;

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
            playerControllers_.PlayerMove.AD_Move.performed += value => inputValue = value.ReadValue<Vector2>();
            //垂直移動
            playerControllers_.PlayerMove.AD_Move.canceled += _ => inputValue = Vector2.zero;

        }
        void Update()
        {

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                IsJump = true;
            }
            UPD_Animator();
            
        }
        void FixedUpdate()
        {
            Move();
        }

        void UPD_Animator()
        {
            //Animator參數
            float speed = inputValue.x * inputValue.x;
            animator.SetFloat("Horizon_Input", speed);

            //改變玩家按下A,D後的角色朝向
            float flipSpeed = 0.1f;
            float Horizontal = Input.GetAxis("Horizontal");
            Vector3 scale = transform.localScale;
            scale.x = Horizontal > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            //左右轉固定朝向
            if ((Horizontal > 0 && scale.x < 0) || (Horizontal < 0 && scale.x > 0))
                transform.localScale = scale;

            //改變動畫
            if (animator)
            {
                // 移動到正確的position
                float moveFactor = Move_speed * Time.deltaTime * 10f;
                MoveCharacter(moveFactor);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, degree, 0), 8 * Time.deltaTime);
        }
        //移動玩家位置
        private void MoveCharacter(float moveFactor)
        {
            //獲取水平輸入值
            // inputValue.x; 
            float Horizontal = Input.GetAxis("Horizontal");
            float Gravity = 15f;
            float JumpHeight = 0f;
            //先將座標歸0再做處理
            Vector3 trans = Vector3.zero;
            //改變方位後移動位置
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
            //Debug.Log(angle);
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

}