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
        // �ˬd¶��ɶ��O�_�W�L�̤j�ɶ�

    //    enemyStatemachine.tranform.LookAt(enemyStatemachine.playerPosition);
        // ����¶��欰
        CircleAroundPlayer(deltaTime);
    }
    public override void Exit()
    {
        Debug.Log("EXIST�Ѣע�Ѣڢעܢ�");
    }
    private void CircleAroundPlayer(float deltaTime)
    {
      
            // ���ĤH���V���a
            FacePlayer();
        
    }
}
