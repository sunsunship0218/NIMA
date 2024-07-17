using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NIMA.Combat;

namespace NIMA.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        public List<AttackSO> combo;

        [SerializeField] float cooldownTime = 0.5f;
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
              ExitAttack();
        }

        void Attack()
        {
            if (Time.time - lastComboEnd > cooldownTime && comboCounter<=combo.Count)
            {
                CancelInvoke("EndCombo");
                if(Time.time - lastclickTime >=cooldownTime)
                {
                    //撥放對應的animation
                    animator.runtimeAnimatorController = combo[comboCounter].animatorOV;
                    if (comboCounter >= 2) // 記得 comboCounter 是從 0 開始
                    {
                        Player.transform.localRotation = Quaternion.Euler(0, -180, 0);
                    }
                    animator.Play("Attack",0,0);
                    //撥放對應的音效
                    if (combo[comboCounter].audioClip != null)
                    {
                        audioSource.clip = (combo[comboCounter].audioClip);
                        audioSource.Play();
                    }
                    weapon.damage = combo[comboCounter].damage;
                    comboCounter++;
                    lastclickTime =Time.time;
                    if (comboCounter +1> combo.Count)
                    {
                        comboCounter = 0;
                    }
                }
            }
        }

        void ExitAttack()
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime>0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                Invoke("EndCombo", 1);
                Player.transform.localRotation = Quaternion.Euler(0, -90, 0);
                
            }
        }
        void EndCombo()
        {
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