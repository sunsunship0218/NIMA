using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCirclingState : EnemyBaseState
{
    public EnemyCirclingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    Vector2 CirclingTimeRange = new Vector2(3, 6);
    float timer = 3f;
    float circlingSpeed = 100f;
    //animator parameter
    readonly int CirclingBlendtreeHASH = Animator.StringToHash("Circling");
    const float animatorDampSpeed = 0.1f;
    readonly int CirclingDirHASH = Animator.StringToHash("circlingDir");
    int circlingDir = 1;
    float circlingRadius = 3f;
    //���}���� is circling

    public override void Enter()
    {
       enemyStatemachine.agent.updatePosition =true;
        enemyStatemachine.agent.updateRotation =true;
        //����ʵe
        enemyStatemachine.animator.CrossFadeInFixedTime(CirclingBlendtreeHASH, 0.1f);
        //��l�Ƥ�V
        circlingDir = Random.Range(0, 2) == 0 ? 1 : -1;
        timer = Random.Range(CirclingTimeRange.x, CirclingTimeRange.y);

    }
    public override void Update(float deltaTime)
    {
        timer -= deltaTime;
     /*   if (timer <= 0)
        {
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
            return;
        }
     /*   if (!IsInCirclingRange())
        {
            enemyStatemachine.SwitchState(new EnemyIdleState(enemyStatemachine));
        }
     */
        // �ˬd¶��ɶ��O�_�W�L�̤j�ɶ�
        // ����¶��欰
        CircleAroundPlayer(deltaTime);
        Debug.Log("ai position : " + enemyStatemachine.transform.position);
    }
    public override void Exit()
    {
        Debug.Log("EXIST�Ѣע�Ѣڢעܢ�");
    }
    private void CircleAroundPlayer(float deltaTime)
    {
       enemyStatemachine.animator.SetFloat(CirclingDirHASH, 0, animatorDampSpeed, deltaTime);
       var offsetPlayer =enemyStatemachine.transform.position -enemyStatemachine.playerStateMachine.transform.position;
        var direction = Vector3.Cross(offsetPlayer, Vector3.up);

        //��ʧ�sAI��m
        enemyStatemachine.agent.destination=(enemyStatemachine.transform.position + direction);
        Move(enemyStatemachine.agent.desiredVelocity.normalized * enemyStatemachine.MovementSpeed, deltaTime);

        Vector3  lookPos = enemyStatemachine.playerStateMachine.transform.position - enemyStatemachine.transform.position;
        lookPos.y = 0;
      Quaternion Rotation =Quaternion.LookRotation(lookPos);
        enemyStatemachine.transform.rotation = Quaternion.Slerp
        (enemyStatemachine.transform.rotation,
        Rotation,
        Time.deltaTime * circlingSpeed);
        FacePlayer();
        
    }

}
