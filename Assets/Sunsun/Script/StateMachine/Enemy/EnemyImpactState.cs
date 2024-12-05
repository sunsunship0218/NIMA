using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    public EnemyImpactState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    readonly int ImpactHASH = Animator.StringToHash("Impact");
    const float crossfadeDuration = 0.14f;
    public override void Enter()
    {
        Debug.Log("enter ENEMY IMPACT");
        enemyStatemachine.animator.CrossFadeInFixedTime(ImpactHASH, crossfadeDuration);
        enemyStatemachine.hitCount++;
        // 更新被击中敌人的 lastHitTime
        enemyStatemachine.lastHitTime = Time.time;
 
    }
    public override void Update(float deltaTime)
    {
        //獲取動畫撥放狀態,防止沒有撥放完就切換狀態
        AnimatorStateInfo currentStateInfo =enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName("Impact") && currentStateInfo.normalizedTime > 0.8f)
        {
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
      
        }
//重置被打中的次數
       if (enemyStatemachine .hitCount> 0 && 
            Time .time- enemyStatemachine.lastHitTime> enemyStatemachine.hitCountResetTime)
        {
            enemyStatemachine.hitCount = 0;
        }
    

    }
    public override void Exit()
    {

    }
}
