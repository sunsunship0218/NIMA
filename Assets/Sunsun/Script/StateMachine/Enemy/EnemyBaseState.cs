using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
      
        return distance > enemyStatemachine.AttackRange * enemyStatemachine.AttackRange
            && distance <= enemyStatemachine.detectionPlayerRange * enemyStatemachine.detectionPlayerRange;


    }
    protected bool IsinShortAttackingRange()
    {

        //����Z��,��������true /false
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        //����Z��,��������true /false       
        return distance <= enemyStatemachine.ShortAttackRange * enemyStatemachine.ShortAttackRange;
    }
    protected bool IsinMidAttackRange()
    {
        //����Z��,��������true /false
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        //����Z��,��������true /false       
        return distance > (enemyStatemachine.ShortAttackRange * enemyStatemachine.ShortAttackRange)
            && distance <= (enemyStatemachine.MidAttackRange * enemyStatemachine.MidAttackRange);
    }
    protected bool IsinLongAttackingRange()
    {

        //����Z��,��������true /false
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        //����Z��,��������true /false       
        return distance > (enemyStatemachine.MidAttackRange * enemyStatemachine.MidAttackRange)
           && distance <= (enemyStatemachine.LongAttackRange * enemyStatemachine.LongAttackRange);
    }
    protected bool IsinAttackingRange()
    {
        return IsinShortAttackingRange() ||  IsinLongAttackingRange() || IsinMidAttackRange();
    }
    protected bool IsInRetreatRange()
    {
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        return distance <= enemyStatemachine.RetreatRange * enemyStatemachine.RetreatRange;
    }
    protected  bool IsInCirclingRange()
    {
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        return distance <= enemyStatemachine.CirclingAroundRange * enemyStatemachine.CirclingAroundRange;
    }
    //�H���v���վ�
    protected bool ShouldAttack()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100f;
        if (healthPercentage < 0.3f)
        {
            return false;
        }

        bool isPlayerVulnerable = !enemyStatemachine.playerStateMachine.playerInputHandler.isBlocking &&
                                  !enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking;
        return IsinAttackingRange();
    
    }
    //�P�_���m������
    protected bool ShouldBlock()
    {
        int rand = Random.Range(0, 10);
        if (!IsinAttackingRange())
        {
            return false;
        }
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100f;
        // �ͩR�ȶV�C�A��׷��v�V��
        if (healthPercentage < 0.5f)
        {
            return rand < 7; // 70% ���v���
        }
        return rand < 5; // 50% ���v���
    }
    //�P�_��h������
    protected bool ShouldRetreat()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100;
        float Cooldown = 2.0f; // ��h���N�o�ɶ�
        float lastTime = -Mathf.Infinity;
        int baseChance = 10;
        // �p�G�b�N�o�ɶ����A���i���h
        if (Time.time < lastTime +Cooldown)
        {
            return false; 
        }
        //�o����Ӧh���|���m
        // �ͩR�ȧC�_50%�A�W�[��h���v
        if (healthPercentage < 0.5f)
        {
            baseChance += 8;
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
        return IsInRetreatRange() && IsinAttackingRange() && retreatChance;
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
