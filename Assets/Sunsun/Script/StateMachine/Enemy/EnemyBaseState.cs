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
    protected  bool IsInCirclingRange()
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
        if (healthPercentage < 0.5f)
        {
            baseChance += 15;
        }
        if (healthPercentage < 0.3f)
        {
            baseChance += 20;
        }
        // ����s������A�W�[���Ҿ��v
        if (enemyStatemachine.hitCount >= 1)
        {
            baseChance += 10;
            enemyStatemachine.hitCount = 0; // ��h���mhitcount
        }
        //���a�������,�W�[���Ҿ��v
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseChance = 95;
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
        float retreatCooldown = 2.0f; // ��h���N�o�ɶ�
        float lastRetreatTime = -Mathf.Infinity;
        int baseChance = 10;
        // �p�G�b�N�o�ɶ����A���i���h
        if (Time.time < lastRetreatTime + retreatCooldown)
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

    protected void ChangeState()
    {
        //  Debug.Log("Retreat? "+ShouldRetreat());
        if (!IsInChasingRange())
        {
            //  Debug.Log("not in chasing range");
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
            return;
        }
        if ( IsInCirclingRange() && enemyStatemachine.CirclingState!=null)
        {
            enemyStatemachine.SwitchState(new EnemyCirclingState(enemyStatemachine));
        }
        else if (ShouldAttack())
        {
            if (enemyStatemachine.AttackingState != null)
            {
                enemyStatemachine.SwitchState(new EnemyAttackingState(enemyStatemachine, 0));
                return;
            }
        }
        else if (ShouldBlock())
        {
            if (enemyStatemachine.BlockState != null)
            {
                enemyStatemachine.SwitchState(new EnemyBlockState(enemyStatemachine));
                return;
            }
        }
        else if (ShouldRetreat())
        {
            if (enemyStatemachine.RetreatState != null)
            {
                enemyStatemachine.SwitchState(new EnemyRetreatState(enemyStatemachine));
                return;
            }

        }
    }
}
