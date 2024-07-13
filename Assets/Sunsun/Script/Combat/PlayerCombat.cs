using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NIMA.Combat;

namespace NIMA.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        //combat combo
        int comboCounter;
        //combo cooldown
        [SerializeField] float cooldownTime = 0.5f;
        [SerializeField] float lastclick= 0f;
        [SerializeField] float lastComboEnd= 0f;

        //character info
        [SerializeField] Weapon currentWeapon;
        Animator animator;
        PlayerControllers playercontrollers;
        [SerializeField] GameObject Player;
        void Awake()
        {
            playercontrollers = new PlayerControllers();
            animator = Player.GetComponent<Animator>();

        }
        void Update()
        {
            if(currentWeapon != null)
            {
                Attack(currentWeapon.weaponName);
                
            }
        }

        void Attack( string weapon)
        {
            //冷卻完後觸發
            if (playercontrollers.Player.Attack.WasPressedThisFrame()&&
                Time.time - lastComboEnd > cooldownTime)
            {
                Debug.Log("Attack Pressed");
                //攻擊一次
                comboCounter++;
                comboCounter= Mathf.Clamp(comboCounter, 0, currentWeapon.comboLength);

                //creat attack  name
                for(int i=0; i<comboCounter; i++) 
                {
                    //第一個攻擊
                    if( i== 0)
                    {
                        if (comboCounter == 1 && animator.GetCurrentAnimatorStateInfo(0).IsTag("Movement"))
                        {
                            animator.SetBool("AttackStart", true);
                            animator.SetBool(weapon+"Attack"+(i+1), true);
                            lastclick =Time.time;
                        }                        
                    }
                    //其他攻擊
                    else
                    {
                        if (comboCounter >= (i + 1) && animator.GetCurrentAnimatorStateInfo(0).IsName(weapon +"Attack"+ i))
                        {
                            animator.SetBool(weapon + "Attack" + (i + 1), true);
                            lastclick = Time.time;
                        }

                    }

                }
            }

            //Animation exit bool reset
            for(int i=0; i<currentWeapon.comboLength;i++) 
            { 
                if ( animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f &&
                    animator.GetCurrentAnimatorStateInfo(0).IsName(weapon+"Attack"+(i+1)))
                {
                    comboCounter = 0;
                    lastComboEnd= Time.time;
                    animator.SetBool(weapon + "Attack" + (i + 1), false);
                    animator.SetBool("AttackStart", false);
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