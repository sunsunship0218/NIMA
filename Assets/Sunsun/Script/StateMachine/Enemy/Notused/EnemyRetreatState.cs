using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRetreatState : EnemyBaseState
{
    public EnemyRetreatState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    readonly int RetreatHASH = Animator.StringToHash("Retreat");
    const float crossfadeDuration = 0.1f;
    const float animatorDampSpeed = 0.14f;
    public override void Enter()
    {
        enemyStatemachine.animator.CrossFadeInFixedTime(RetreatHASH, crossfadeDuration);
        Debug.Log("Enter Retreat 進入退後");
    }
    public override void Update(float deltaTime)
    {
        // 更新角色控制器以確保位置更新
        MoveWithDeltatime(deltaTime);
        //獲取動畫撥放狀態,防止沒有撥放完就切換狀態
        AnimatorStateInfo currentStateInfo = enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName("Retreat") && currentStateInfo.normalizedTime >0.8f)
        {
           enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine)); 
         }
        if (!IsInRetreatRange())
        {
            Debug.Log("not in retreat range");
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
            return;
        }
        RetreatFromPlayer(deltaTime);
        FacePlayer();
    }

    public override void Exit()
    {
       
    }

    void RetreatFromPlayer(float deltaTime)
    {
   
        if (enemyStatemachine.agent.isOnNavMesh)
        {
            Vector3 directionAwayFromPlayer = (enemyStatemachine.transform.position - enemyStatemachine.player.transform.position).normalized;
            Vector3 retreatPosition = enemyStatemachine.transform.position + directionAwayFromPlayer * enemyStatemachine.RetreatRange;

            enemyStatemachine.agent.destination = retreatPosition;
            Move(enemyStatemachine.agent.desiredVelocity.normalized * enemyStatemachine.MovementSpeed, deltaTime);

            // 同步 NavMeshAgent 的位置
            enemyStatemachine.agent.nextPosition = enemyStatemachine.transform.position;
        }
    }
}
