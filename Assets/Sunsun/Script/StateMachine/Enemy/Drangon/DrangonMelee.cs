using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrangonMelee : EnemyStateMachine
{
    void Start()
    {
        // 初始化共用的狀態
        IdleState = new EnemyIdleState(this);
        CirclingState = new EnemyCirclingState(this);
        ChasingState = new EnemyChasingState(this);
        DeadState = new EnemyDeadState(this);
        // 初始化特定的狀態
        AttackingState = new EnemyAttackingState(this,0);
        BlockState = new EnemyBlockState(this);
        RetreatState = new EnemyRetreatState(this);

        // 切換到初始狀態
        SwitchState(IdleState);
    }
}
