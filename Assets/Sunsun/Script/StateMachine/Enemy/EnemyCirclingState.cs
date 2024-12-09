using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCirclingState : EnemyBaseState
{
    public EnemyCirclingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    //不是座標,是數組作為處存隨機數,1-3秒
    Vector2 CirclingTimeRange = new Vector2(1,3);
    float timer ;
    //animator parameter
    readonly int CirclingBlendtreeHASH = Animator.StringToHash("Circling");
    const float animatorDampSpeed = 0.1f;
    readonly int CirclingDirHASH = Animator.StringToHash("circlingDir");
    int circlingDir = 1;
    //離開條件 is circling
    bool isTimerInitialized = false;
    public override void Enter()
    {
        if (!isTimerInitialized)
        {
            enemyStatemachine.lastCirclingTime = Time.time;
            //撥放動畫
            enemyStatemachine.animator.CrossFadeInFixedTime(CirclingBlendtreeHASH, 0.1f);
            //初始化方向
            circlingDir = Random.Range(0, 2) == 0 ? 1 : -1;
            timer = Random.Range(CirclingTimeRange.x, CirclingTimeRange.y);
        }
  

    }
    public override void Update(float deltaTime)
    {
        timer -= deltaTime;
        if (timer <0 && IsInCirclingRange())
        {
            enemyStatemachine.SwitchState(new EnemyAttackingState(enemyStatemachine, 0));
            return;
        }
        else if (IsinAttackingRange())  
        {
            enemyStatemachine.SwitchState(new EnemyAttackingState(enemyStatemachine, 0));
            return;
        }
    
        
        if (!IsInCirclingRange())
        {
            enemyStatemachine.SwitchState(new EnemyChasingState(enemyStatemachine));
        }
    
        // 檢查繞行時間是否超過最大時間
        // 執行繞行行為
        CircleAroundPlayer(deltaTime);
     
    }
    public override void Exit()
    {
        isTimerInitialized = false;
    }
    private void CircleAroundPlayer(float deltaTime)
    {
       enemyStatemachine.animator.SetFloat(CirclingDirHASH, 0, animatorDampSpeed, deltaTime);
       var offsetPlayer =enemyStatemachine.transform.position -enemyStatemachine.playerStateMachine.transform.position;
        var direction = Vector3.Cross(offsetPlayer, Vector3.up);

        //手動更新AI位置
        if (enemyStatemachine.agent.isOnNavMesh)
        {
            enemyStatemachine.agent.destination = (enemyStatemachine.transform.position + direction);
         Move(enemyStatemachine.agent.desiredVelocity.normalized * enemyStatemachine.circlingSpeed, deltaTime);
        }
        Vector3  lookPos = enemyStatemachine.playerStateMachine.transform.position - enemyStatemachine.transform.position;
        lookPos.y = 0;
      Quaternion Rotation =Quaternion.LookRotation(lookPos);
        enemyStatemachine.transform.rotation = Quaternion.Slerp
        (enemyStatemachine.transform.rotation,
        Rotation,
        Time.deltaTime * enemyStatemachine.circlingSpeed);
        FacePlayer();
        
    }

}
