using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : EnemyStateMachine
{
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
    private void HandleHitCountCheck()
    {
        if (hitCount > 2)
        {
            SwitchState(BlockState);
            hitCount = 0; // ���m?����?
        }
    }
}
