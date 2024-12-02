using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCirclingState : EnemyBaseState
{
    public EnemyCirclingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    float timer = 0f;
    public override void Enter()
    {
        Debug.Log("ENTER CIRCLING");
      
    }
    public override void Update(float deltaTime)
    {
        if (!IsInCirclingRange())
        {
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
        }
        // 檢查繞行時間是否超過最大時間

    //    enemyStatemachine.tranform.LookAt(enemyStatemachine.playerPosition);
        // 執行繞行行為
        CircleAroundPlayer(deltaTime);
    }
    public override void Exit()
    {
        Debug.Log("EXISTＣＩＲＣＬＩＮＧ");
    }
    private void CircleAroundPlayer(float deltaTime)
    {
      
            // 讓敵人面向玩家
            FacePlayer();
        
    }
}
