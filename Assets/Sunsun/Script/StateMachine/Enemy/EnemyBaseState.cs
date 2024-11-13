using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState :State
{
    protected EnemyStateMachine enemyStatemachine;
    public EnemyBaseState(EnemyStateMachine enemyStatemachine)
    {
        this.enemyStatemachine = enemyStatemachine;
    }

    //�ˬd�򪱮a���Z��,�M�w����/�l�v ����,�w�]��idle
    protected  bool IsInChasingRange( )
    {
      float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
     return  distance <= enemyStatemachine.detectionPlayerRange *enemyStatemachine.detectionPlayerRange;
    }
    protected bool IsinAttackingRange()
    {
        
        //����Z��,��������true /false
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        //����Z��,��������true /false       
        return distance <= enemyStatemachine.AttackRange * enemyStatemachine.AttackRange;
    }
    protected bool IsInRetreatRange()
    {
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        return distance <= enemyStatemachine.RetreatRange * enemyStatemachine.RetreatRange;
    }
    //��L�P�_����������
    protected bool ShouldAttack()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth()/100;
        int baseAttackChance = 10;
        if (healthPercentage > 0.7f)
        // �ͩR�Ȱ�>70%�A�W�[�������v
        {
            baseAttackChance += 20;
        }

        // ���a���b��?�A���C��?���v
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseAttackChance -= 20;
        }

        // ����v�b0-100%
        baseAttackChance = Mathf.Clamp(baseAttackChance, 0, 100);

        int rand = Random.Range(0, 100);
        bool attackChance = rand < baseAttackChance;
        return IsinAttackingRange() && attackChance;
    }
    //�P�_���m������
    protected bool ShouldBlock()
    {
       
        //�W�[�H�����v
        int rand = Random.Range(0, 100);
        bool blockChance = rand < 40;
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            blockChance = rand < 80;
        }
      //  Debug.Log(blockChance);
        return IsinAttackingRange() && blockChance;
    }
    //�P�_��h������
    protected bool ShouldRetreat()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100;
        int baseRetreatChance = 10;

        // �ͩR�ȧC�_50%�A�W�[�Z�h���v
        if (healthPercentage < 0.5f)
        {
            baseRetreatChance += 20;
        }

        // �ͩR�ȧC�_30%�A?�@�B�W�[�Z�h���v
        if (healthPercentage < 0.3f)
        {
            baseRetreatChance += 20;
        }

        // ����??��?�A�W�[�Z�h���v
        if (enemyStatemachine.hitCount >= 3)
        {
            baseRetreatChance += 30;
            enemyStatemachine.hitCount = 0; // ���m��???��
        }

        // ���a���b��?�A�W�[�Z�h���v
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseRetreatChance += 20;
        }

        // ����v�b0-100%
        baseRetreatChance = Mathf.Clamp(baseRetreatChance, 0, 100);

        int rand = Random.Range(0, 100);
        bool retreatChance = rand < baseRetreatChance;
        return IsInRetreatRange() &&IsinAttackingRange() &&retreatChance;
    }
    //�P�_¶�檺����
    protected bool ShouldCircleAround()
    {
        return true;
    }
    protected void Move(Vector3 motion, float deltatime)
    {
        enemyStatemachine.characterController.Move((motion + enemyStatemachine.forceReceiver.movement) * deltatime);
    }

    protected void MoveWithDeltatime(float deltatime)
    {
        Move(Vector3.zero, deltatime);
    }
    //��ʰ���
    protected void FacePlayer()
    {
        if (enemyStatemachine.player == null) { return; }
        Vector3 faceTargetPos;
        faceTargetPos = enemyStatemachine.player.transform.position - enemyStatemachine.transform.position;
        faceTargetPos.y = 0f;
      enemyStatemachine.transform.rotation = Quaternion.LookRotation(faceTargetPos);
    }
}
