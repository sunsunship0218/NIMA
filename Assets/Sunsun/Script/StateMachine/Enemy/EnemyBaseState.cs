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

    //��L�P�_����������
    protected bool ShouldAttack()
    {
        //�W�[�H�����v
        int rand = Random.Range(0, 100);
        bool attackChance = rand < 60;
        return IsinAttackingRange() && attackChance;
    }
    //�P�_����������
    protected bool ShouldBlock()
    {
        //�W�[�H�����v
        int rand = Random.Range(0, 100);
        bool blockChance = rand < 40;
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            blockChance = rand < 80;
        }
        return IsinAttackingRange() && blockChance;
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
