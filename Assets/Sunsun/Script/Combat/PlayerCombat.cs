using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NIMA.Combat;

namespace NIMA.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        public List<AttackSO> combo;
        public bool isAttacking;

        [SerializeField] float cooldownTime = 0.1f;
        float lastclickTime;
        float lastComboEnd;
        int comboCounter;

        Animator animator;
        AudioSource audioSource;
        [SerializeField] Weapon weapon;
      
        PlayerControllers playercontrollers;
        [SerializeField] GameObject Player;
        void Awake()
        {
            playercontrollers = new PlayerControllers();
            animator=Player.GetComponent<Animator>();
            audioSource = Player.GetComponent<AudioSource>();

        }
        void Update()
        {
            if (playercontrollers.Player.Attack.WasPressedThisFrame())
            {
                Attack();
            }
            if(playercontrollers.Player.Move.WasPressedThisFrame())
            {
                CancelAttack();
            }
            ExitAttack();
        }

        void Attack()
        {
         
            if (Time.time - lastComboEnd > cooldownTime && comboCounter<=combo.Count)
            {
                CancelInvoke("EndCombo");
                if(Time.time - lastclickTime >=cooldownTime)
                {
                    isAttacking = true;
                    //撥放對應的animation
                    animator.runtimeAnimatorController = combo[comboCounter].animatorOV;
                  //  Debug.Log(comboCounter);                  
                    animator.Play("Attack",0,0);
                    if (0 >= comboCounter)
                    {
                        Player.transform.localRotation = Quaternion.Euler(0, -180, 0);
                    }

                    //撥放對應的音效
                    if (combo[comboCounter].audioClip != null)
                    {
                        //替換掉相應的音源
                        audioSource.clip = (combo[comboCounter].audioClip);
                        audioSource.Play();
                    }
                    weapon.damage = combo[comboCounter].damage;
                    comboCounter++;
                    lastclickTime =Time.time;
                    if (comboCounter + 1 > combo.Count)
                    {
                        comboCounter = 0;
                    }
                }
            }
        }

        void CancelAttack()
        {
            if (isAttacking==true)
            {
                isAttacking = false;
                comboCounter = 0;
                lastComboEnd = Time.time; Player.transform.localRotation = Quaternion.Euler(0, -90, 0);
                animator.CrossFade("Movement", 0.1f);

            }
        }

        void ExitAttack()
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                Invoke("EndCombo", 1);
               isAttacking=false;
               Player.transform.localRotation = Quaternion.Euler(0, -90, 0);
              


            }
        }
        void EndCombo()
        {
            isAttacking = false;
            comboCounter = 0;
            lastComboEnd= Time.time;                        
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