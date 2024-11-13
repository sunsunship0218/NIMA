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
    protected bool IsInRetreatRange()
    {
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        return distance <= enemyStatemachine.RetreatRange * enemyStatemachine.RetreatRange;
    }
    //其他判斷攻擊的條件
    protected bool ShouldAttack()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth()/100;
        int baseAttackChance = 10;
        if (healthPercentage > 0.7f)
        // 生命值高>70%，增加攻擊概率
        {
            baseAttackChance += 20;
        }

        // 玩家正在攻?，降低攻?概率
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseAttackChance -= 20;
        }

        // 限制概率在0-100%
        baseAttackChance = Mathf.Clamp(baseAttackChance, 0, 100);

        int rand = Random.Range(0, 100);
        bool attackChance = rand < baseAttackChance;
        return IsinAttackingRange() && attackChance;
    }
    //判斷防禦的條件
    protected bool ShouldBlock()
    {
       
        //增加隨機概率
        int rand = Random.Range(0, 100);
        bool blockChance = rand < 40;
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            blockChance = rand < 80;
        }
      //  Debug.Log(blockChance);
        return IsinAttackingRange() && blockChance;
    }
    //判斷後退的條件
    protected bool ShouldRetreat()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100;
        int baseRetreatChance = 10;

        // 生命值低于50%，增加后退概率
        if (healthPercentage < 0.5f)
        {
            baseRetreatChance += 20;
        }

        // 生命值低于30%，?一步增加后退概率
        if (healthPercentage < 0.3f)
        {
            baseRetreatChance += 20;
        }

        // 受到??攻?，增加后退概率
        if (enemyStatemachine.hitCount >= 3)
        {
            baseRetreatChance += 30;
            enemyStatemachine.hitCount = 0; // 重置受???器
        }

        // 玩家正在攻?，增加后退概率
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseRetreatChance += 20;
        }

        // 限制概率在0-100%
        baseRetreatChance = Mathf.Clamp(baseRetreatChance, 0, 100);

        int rand = Random.Range(0, 100);
        bool retreatChance = rand < baseRetreatChance;
        return IsInRetreatRange() &&IsinAttackingRange() &&retreatChance;
    }
    //判斷繞行的條件
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
