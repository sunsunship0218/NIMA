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
        int baseChance = 10;
        if (healthPercentage > 0.7f)
        // 生命值高>70%，增加攻擊機率
        {
            baseChance += 20;
        }
        // 玩家正在攻擊,降低機率
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseChance -= 20;
        }
        // 限制概率在0-100
        baseChance = Mathf.Clamp(baseChance, 0, 100);
        int rand = Random.Range(0, 100);
        bool attackChance = rand < baseChance;
        return IsinAttackingRange() && attackChance;
    }
    //判斷防禦的條件
    protected bool ShouldBlock()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100;
        int baseChance = 20;
        //生命值大於80,減少防禦
        if (healthPercentage > 0.7f)     
        {
            baseChance -= 5;
        }
        //玩家持續攻擊,增加格黨機率
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseChance += 80;
        }
        // 格黨防禦攻擊減少概率
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
    //判斷後退的條件
    protected bool ShouldRetreat()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100;
        int baseChance = 10;

        // 生命值低于50%，增加後退機率
        if (healthPercentage < 0.5f)
        {
            baseChance += 8;
        }

        // 生命值低于30%，進一步家後退機率
        if (healthPercentage < 0.3f)
        {
            baseChance += 10;
        }

        // 受到連續攻擊，增加後退機率
        if (enemyStatemachine.hitCount >= 1)
        {
            baseChance += 9;
            enemyStatemachine.hitCount = 0; // 後退重置hitcount
        }

        // 玩家正在攻擊,增加機率
        if (enemyStatemachine.playerStateMachine.playerInputHandler.isAttacking)
        {
            baseChance += 5;
        }

        // 限制機率在0-100%
        baseChance = Mathf.Clamp(baseChance, 0, 100);

        int rand = Random.Range(0, 100);
        bool retreatChance = rand < baseChance;
        return IsInRetreatRange() &&IsinAttackingRange() &&retreatChance;
    }
    //判斷繞行的條件
    protected bool ShouldCircleAround()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100;
        int baseChance = 50;

      //生命高 繞行
        if (healthPercentage > 0.8f)
        {
            baseChance += 20;
        }

        // 玩家攻擊,尋找機會
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
