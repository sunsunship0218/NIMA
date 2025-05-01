using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadState : BossBaseState
{
    bool isDestroyed = false;

    public BossDeadState(BossStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    readonly int DeadHASH = Animator.StringToHash("Dead");
    const float crossfadeDuration = 0.14f;
    float duration;
    public override void Enter()
    {
        enemyStatemachine.animator.CrossFadeInFixedTime(DeadHASH, crossfadeDuration);
        //�T�ΪZ��
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
            //������O����
            GameObject.Destroy(enemyStatemachine.gameObject);

        }
    }
    public override void Exit()
    {

    }
}
