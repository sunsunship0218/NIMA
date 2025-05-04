using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AttackBarController : MonoBehaviour
{
    float attackBarValue = 0f;         // ��e�������ƭ�
    float maxAttackBarValue = 100f;      // ���������Ȫ��]�w
     float increaseAmount = 20f;          // �C�������ĤH�W�[���ƭȡ]�i�ھڻݨD�վ�^
     float decayRate = 10f;               // �C��I��ƭȶq

    // ����O�_���\�ֿn�������ƭ�
    // �w�]�i�H�ֿn�A��ֿn�캡�ȫ�|�]�w�� false�A
    // �A���ȰI��� 0 �ɭ��s�}��ֿn�C
    bool canAccumulate = true;
    private float healTimer = 0f;
    bool isBarFull = false;
    // �w�q����������ȮɭnĲ�o���ƥ�
    public static event Action OnAttackBarFull;

   [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerStateMachine playerStateMachine;
    [SerializeField] Slider attackSilder;


    private void Update()
    {
        // �p�G�ثe�����\�ֿn�]�Y�i�J�N�o���A�^�B�������ƭȩ|���k�s�A�N����I��
        if (!canAccumulate && attackBarValue > 0f)
        {
            attackBarValue -= decayRate * Time.deltaTime;
            if (attackBarValue < 0f)
                attackBarValue = 0f;
        }

        // ������������I� 0 �ɡA��_�ֿn
        if (attackBarValue == 0f)
        {
            canAccumulate = true;
            isBarFull = false;
        }

        // �C�V��s Slider �����
        if (attackSilder != null)
            attackSilder.value = attackBarValue / maxAttackBarValue;
    }
  

    void HandleEnemyHit()
    {
        // �p�G�ثe���\�ֿn�A�N����֥[�޿�
        if (canAccumulate && playerStateMachine.playerInputHandler.GetIsAttacking()==true)
        {
        //    Debug.Log("Increasing attack bar");
            attackBarValue += increaseAmount;
            if (attackBarValue >= maxAttackBarValue)
            {
                attackBarValue = maxAttackBarValue;
                isBarFull = true;
                // �ֿn�캡�ȫ�A�ߧY�T��ֿn�A���ݧ����I�h�� 0
                canAccumulate = false;
                // Ĳ�o���������Ȩƥ�
                OnAttackBarFull?.Invoke();
                if (attackSilder != null)
                    attackSilder.value = 1f;  // ��ܺ�
            }
            else
            {
                // �֥[���s UI
                if (attackSilder != null)
                    attackSilder.value = attackBarValue / maxAttackBarValue;
            }
        }
        else
        {
            // �b�N�o�����]���i�ֿn�ɡ^�A�Y�ϧ����I���o�ͤ]���W�[�������ƭ�
            // ���p�G�ݭn�b��������Ĳ�o�^��]�Ҧp���󺡨��ɡ^�A�i�H�O�d�νվ�H�U�޿�G
            if (playerHealth != null && attackBarValue > 0f)
            {
                playerHealth.healthSystem.HealAmount(10);
           //     Debug.Log("Player healed during cooldown");
            }
        }
    }

    private void OnEnable()
    {
      
        WeaponDamage.OnEnemyHit +=HandleEnemyHit;
    }

    private void OnDisable()
    {
      
        WeaponDamage.OnEnemyHit -= HandleEnemyHit;
    }

}
