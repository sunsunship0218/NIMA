using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    bool isDestroyed = false;

    public EnemyDeadState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    readonly int DeadHASH = Animator.StringToHash("Dead");
    const float crossfadeDuration = 0.14f;
    float duration;
    public override void Enter()
    {
        enemyStatemachine.animator.CrossFadeInFixedTime(DeadHASH, crossfadeDuration);
        //禁用武器
        enemyStatemachine.weaponDamageL.gameObject.SetActive(false);
        enemyStatemachine.weaponDamageR.gameObject.SetActive(false);
    

    }
    public override void Update(float deltaTime)
    {
        AnimatorStateInfo currentStateInfo = enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        bool isAnimationFinished = currentStateInfo.normalizedTime >= 0.95f;
        if (isAnimationFinished)
        {
            isDestroyed = true;
            //物件池記的換
            GameObject.Destroy(enemyStatemachine.gameObject);
     
        }
    }
    public override void Exit()
    {
    
    }
}
