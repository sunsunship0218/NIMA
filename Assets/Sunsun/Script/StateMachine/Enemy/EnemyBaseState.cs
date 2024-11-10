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

    //檢查跟玩家的距離,決定攻擊/追逐 巡邏,預設為idle
    protected  bool IsInChasingRange( )
    {
      float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
     return  distance <= enemyStatemachine.detectionPlayerRange *enemyStatemachine.detectionPlayerRange;
    }
    protected bool IsinAttackingRange()
    {
        
        //比較距離,直接給予true /false
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        //比較距離,直接給予true /false
       
        return distance <= enemyStatemachine.AttackRange * enemyStatemachine.AttackRange;
    }

    //其他判斷攻擊的條件
    protected bool ShouldAttack()
    {
        //增加隨機概率
        int rand = Random.Range(0, 100);
        bool attackChance = rand < 60;
        return IsinAttackingRange() && attackChance;
    }
    //判斷攻擊的條件
    protected bool ShouldBlock()
    {
        //增加隨機概率
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
    //手動偵測
    protected void FacePlayer()
    {
        if (enemyStatemachine.player == null) { return; }
        Vector3 faceTargetPos;
        faceTargetPos = enemyStatemachine.player.transform.position - enemyStatemachine.transform.position;
        faceTargetPos.y = 0f;
      enemyStatemachine.transform.rotation = Quaternion.LookRotation(faceTargetPos);
    }
}
