using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tiger : EnemyStateMachine
{
    public static event Action OnTigerDestroyed;

    void Start()
    {
        // ��l�Ʀ@�Ϊ����A
        IdleState = new EnemyIdleState(this);
        CirclingState =new  EnemyCirclingState(this);
        ChasingState = new EnemyChasingState(this);
        DeadState = new EnemyDeadState(this);
        // ��l�ƯS�w�����A
        AttackingState = new EnemyAttackingState(this, 0);
        BlockState = new EnemyBlockState(this);
        //RetreatState = new EnemyRetreatState(this);

        // �������l���A
        SwitchState(IdleState);
        // �q�\���������ˬd�ƥ�
        health.healthSystem.OnTakeDamage += HandleHitCountCheck;
    }
    //�W�[��׭�
    private void HandleHitCountCheck()
    {
        if (hitCount > 2)
        {
            SwitchState(BlockState);
            hitCount = 0; // ���m?����?
        }
    }
    void OnDestroy()
    {
        //�q�������V�ĤH��List����
        this.playerStateMachine.EnemyList.Remove(this.gameObject);
        //��_���a��q
       this.playerStateMachine.playerHealth.healthSystem.HealAmount(45);
        Debug.Log(this.playerStateMachine.playerHealth.healthSystem.ReturnHealth());
        //�޵o�ƥ�
        OnTigerDestroyed?.Invoke();
    }

}
