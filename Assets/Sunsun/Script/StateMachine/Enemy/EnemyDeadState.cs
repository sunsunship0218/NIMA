using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    public override void Enter()
    {
        enemyStatemachine.ragDoll.toogleRagdoll(true);
        //�T�ΪZ��
        enemyStatemachine.weaponDamageL.gameObject.SetActive(false);
        enemyStatemachine.weaponDamageR.gameObject.SetActive(false);
        //�P���Ҧ��ؼ�
        Object.Destroy(enemyStatemachine.target);

    }
    public override void Update(float deltaTime)
    {

    }
    public override void Exit()
    {

    }
}
