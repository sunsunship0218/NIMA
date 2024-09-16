using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    readonly int DeadHASH = Animator.StringToHash("Dead");
    const float crossfadeDuration = 0.14f;
    float duration;
    public override void Enter()
    {
        enemyStatemachine.ragDoll.toogleRagdoll(true);
        //禁用武器
        enemyStatemachine.weaponDamageL.gameObject.SetActive(false);
        enemyStatemachine.weaponDamageR.gameObject.SetActive(false);
        //銷毀所有目標
        GameObject.Destroy(enemyStatemachine.target);

    }
    public override void Update(float deltaTime)
    {

    }
    public override void Exit()
    {

    }
}
