using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossImpactState : BossBaseState
{
    public BossImpactState(BossStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    readonly int ImpactHASH = Animator.StringToHash("Impact");
    const float crossfadeDuration = 0.14f;
    public override void Enter()
    {
        enemyStatemachine.animator.CrossFadeInFixedTime(ImpactHASH, crossfadeDuration);
        enemyStatemachine.hitCount++;
        // ��s�Q?��?�H�� lastHitTime
        enemyStatemachine.lastHitTime = Time.time;

    }
    public override void Update(float deltaTime)
    {
        //����ʵe���񪬺A,����S�����񧹴N�������A
        AnimatorStateInfo currentStateInfo = enemyStatemachine.animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName("Impact") && currentStateInfo.normalizedTime > 0.8f)
        {
            enemyStatemachine.SwitchState(new BossIdleState(enemyStatemachine));

        }
        //���m�Q����������
        if (enemyStatemachine.hitCount > 0 &&
             Time.time - enemyStatemachine.lastHitTime > enemyStatemachine.hitCountResetTime)
        {
            enemyStatemachine.hitCount = 0;
        }


    }
    public override void Exit()
    {

    }
}
