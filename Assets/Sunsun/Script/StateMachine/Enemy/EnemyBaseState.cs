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

    //檢查跟玩家的距離,決定攻擊/追逐 巡邏,預設為idle
    protected  bool IsInChasingRange( )
    {
       float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
      
        return distance > enemyStatemachine.AttackRange * enemyStatemachine.AttackRange
            && distance <= enemyStatemachine.detectionPlayerRange * enemyStatemachine.detectionPlayerRange;


    }
    protected bool IsinShortAttackingRange()
    {

        //比較距離,直接給予true /false
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        //比較距離,直接給予true /false       
        return distance <= enemyStatemachine.ShortAttackRange * enemyStatemachine.ShortAttackRange;
    }
    protected bool IsinMidAttackRange()
    {
        //比較距離,直接給予true /false
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        //比較距離,直接給予true /false       
        return distance > (enemyStatemachine.ShortAttackRange * enemyStatemachine.ShortAttackRange)
            && distance <= (enemyStatemachine.MidAttackRange * enemyStatemachine.MidAttackRange);
    }
    protected bool IsinLongAttackingRange()
    {

        //比較距離,直接給予true /false
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        //比較距離,直接給予true /false       
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
    //隨機權重調整
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
    //判斷防禦的條件
    protected bool ShouldBlock()
    {
        int rand = Random.Range(0, 10);
        if (!IsinAttackingRange())
        {
            return false;
        }
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100f;
        // 生命值越低，格擋概率越高
        if (healthPercentage < 0.5f)
        {
            return rand < 7; // 70% 概率格擋
        }
        return rand < 5; // 50% 概率格擋
    }
    //判斷後退的條件
    protected bool ShouldRetreat()
    {
        float healthPercentage = enemyStatemachine.health.healthSystem.ReturnHealth() / 100;
        float Cooldown = 2.0f; // 後退的冷卻時間
        float lastTime = -Mathf.Infinity;
        int baseChance = 10;
        // 如果在冷卻時間內，不進行後退
        if (Time.time < lastTime +Cooldown)
        {
            return false; 
        }
        //這邊切太多不會防禦
        // 生命值低于50%，增加後退機率
        if (healthPercentage < 0.5f)
        {
            baseChance += 8;
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
