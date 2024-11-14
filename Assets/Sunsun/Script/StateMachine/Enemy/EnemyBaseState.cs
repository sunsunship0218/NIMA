using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        int baseChance = 10;
        if (healthPercentage > 0.7f)
        // �ͩR�Ȱ�>70%�A�W�[�������v
        {
            baseChance += 20;
        }
        // ���a���b����,���C���v
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseChance -= 20;
        }
        // ����v�b0-100
        baseChance = Mathf.Clamp(baseChance, 0, 100);
        int rand = Random.Range(0, 100);
        bool attackChance = rand < baseChance;
        return IsinAttackingRange() && attackChance;
    }
    //�P�_���m������
    protected bool ShouldBlock()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100;
        int baseChance = 20;
        //�ͩR�Ȥj��80,��֨��m
        if (healthPercentage > 0.7f)     
        {
            baseChance -= 5;
        }
        //���a�������,�W�[���Ҿ��v
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseChance += 80;
        }
        // ���Ҩ��m������ַ��v
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isBlocking ||
            enemyStatemachine.playerStateMachine.playerInputHandler.isParrying ||
            enemyStatemachine.playerStateMachine.playerInputHandler.isDashing)
        {
            baseChance -= 30;
        }
        int rand = Random.Range(0, 100);
        bool blockChance = rand < baseChance;
      //  Debug.Log(blockChance);
        return IsinAttackingRange() && blockChance;
    }
    //�P�_��h������
    protected bool ShouldRetreat()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100;
        int baseChance = 10;

        // �ͩR�ȧC�_50%�A�W�[��h���v
        if (healthPercentage < 0.5f)
        {
            baseChance += 8;
        }

        // �ͩR�ȧC�_30%�A�i�@�B�a��h���v
        if (healthPercentage < 0.3f)
        {
            baseChance += 10;
        }

        // ����s������A�W�[��h���v
        if (enemyStatemachine.hitCount >= 1)
        {
            baseChance += 9;
            enemyStatemachine.hitCount = 0; // ��h���mhitcount
        }

        // ���a���b����,�W�[���v
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseChance += 5;
        }

        // ������v�b0-100%
        baseChance = Mathf.Clamp(baseChance, 0, 100);

        int rand = Random.Range(0, 100);
        bool retreatChance = rand < baseChance;
        return IsInRetreatRange() &&IsinAttackingRange() &&retreatChance;
    }
    //�P�_¶�檺����
    protected bool ShouldCircleAround()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100;
        int baseChance = 50;

      //�ͩR�� ¶��
        if (healthPercentage > 0.8f)
        {
            baseChance += 20;
        }

        // ���a����,�M����|
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isBlocking)
        {
            baseChance += 30;
        }

        int rand = Random.Range(0, 100);
        bool circleChance = rand < baseChance;
        return circleChance;
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
