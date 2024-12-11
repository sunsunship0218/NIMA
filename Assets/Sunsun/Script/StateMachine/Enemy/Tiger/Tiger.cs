using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tiger : EnemyStateMachine
{
    public static event Action OnTigerDestroyed;

    void Start()
    {
        // 初始化共用的狀態
        IdleState = new EnemyIdleState(this);
        CirclingState =new  EnemyCirclingState(this);
        ChasingState = new EnemyChasingState(this);
        DeadState = new EnemyDeadState(this);
        // 初始化特定的狀態
        AttackingState = new EnemyAttackingState(this, 0);
        BlockState = new EnemyBlockState(this);
        //RetreatState = new EnemyRetreatState(this);

        // 切換到初始狀態
        SwitchState(IdleState);
        // 訂閱擊中次數檢查事件
        health.healthSystem.OnTakeDamage += HandleHitCountCheck;
    }
    //增加格擋值
    private void HandleHitCountCheck()
    {
        if (hitCount > 2)
        {
            SwitchState(BlockState);
            hitCount = 0; // 重置?中次?
        }
    }
    void OnDestroy()
    {
        OnTigerDestroyed?.Invoke();
    }

}
