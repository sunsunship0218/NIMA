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
        [SerializeField] float cooldownTime =0.5f;
        [SerializeField] float nextFiretime = 1f;
        [SerializeField] float  lastclickTime = 0f;
        [SerializeField] float maxCombodelay = 1f;
        public  int noOfClicks = 0;

        void Awake()
        {
            playercontrollers = new PlayerControllers();
            animator = Player.GetComponent<Animator>();

        }
        private void Update()
        {
            Playoff();
            if (playercontrollers.Player.Attack.WasPressedThisFrame())
            {              
                Attacking();
            }       
          
        }

        void Attacking()
        {
     
            lastclickTime = Time.time;
           noOfClicks++;
            if (noOfClicks == 1)
            {             
                animator.SetBool("Light", true);
                
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

            if(noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime> 0.7f
                && animator.GetCurrentAnimatorStateInfo(0).IsName("Light"))
            {
                animator.SetBool("Light", false);
                Debug.Log("LF");
                animator.SetBool("Medium", true);
                Debug.Log("MT");
            }

            if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
                && animator.GetCurrentAnimatorStateInfo(0).IsName("Medium"))
            {
                animator.SetBool("Medium", false);
                Debug.Log("MF");
                animator.SetBool("Heavy", true);
                Debug.Log("MT");
            }
        }
       
        void Playoff()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
              && animator.GetCurrentAnimatorStateInfo(0).IsName("Light"))
            {
                animator.SetBool("Light", false);
                Debug.Log("LightFalse");
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("Medium"))
            {
                animator.SetBool("Medium", false);
                          
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("Heavy"))
            {
                animator.SetBool("Heavy", false);
                noOfClicks = 0;
            }

          if(Time.time - lastclickTime > maxCombodelay)
            {
                noOfClicks = 0;           
            }
            if (Time.time > nextFiretime)
            {
                if (playercontrollers.Player.Attack.WasPressedThisFrame())
                {
                    Attacking();
                }
            }
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