using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NIMA.Combat
{
    public class Fight : MonoBehaviour
    {
        PlayerControllers playercontrollers;
        Animator animator;
       [SerializeField] GameObject Player;
       [SerializeField]  GameObject npc;
        void Awake()
        {
            playercontrollers = new PlayerControllers();
            animator = Player.GetComponent<Animator>();

        }
        private void Update()
        {
            if (playercontrollers.Player.Attack.WasPressedThisFrame())
            {              
                Attacking();
            }
            if (playercontrollers.Player.Attack.WasReleasedThisFrame())
            {
                // 按键在当前帧释放
                animator.SetBool("Attacking", false);
            }

            
            
           
        }

      
        void Attacking()
        {
            animator.SetBool("Attacking",true);
        }
       
        void OnEnable()
        {
            playercontrollers.Player.Enable();
        }
        void OnDisable()
        {
            playercontrollers.Player.Disable();
        }
    }
}