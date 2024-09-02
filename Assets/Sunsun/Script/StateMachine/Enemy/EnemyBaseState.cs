using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState :State
{
    protected EnemyStateMachine enemyStatemachine;
    public EnemyBaseState(EnemyStateMachine enemyStatemachine)
    {
        this.enemyStatemachine = enemyStatemachine;
    }

    //ÀË¬d¸òª±®aªº¶ZÂ÷
    protected  bool IsInChasingRange( )
    {
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
     return  distance <= enemyStatemachine.detectionPlayerRange *enemyStatemachine.detectionPlayerRange;
    }
    protected bool IsinAttackingRange()
    {
        float distance = (enemyStatemachine.player.transform.position - enemyStatemachine.transform.position).sqrMagnitude;
        return distance <= enemyStatemachine.AttackRange * enemyStatemachine.AttackRange;
    }

    protected void Move(Vector3 motion, float deltatime)
    {
        enemyStatemachine.characterController.Move((motion + enemyStatemachine.forceReceiver.movement) * deltatime);
    }

    protected void MoveWithDeltatime(float deltatime)
    {
        Move(Vector3.zero, deltatime);
    }

    protected void FacePlayer()
    {
        if (enemyStatemachine.player == null) { return; }
        Vector3 faceTargetPos;
        faceTargetPos = enemyStatemachine.player.transform.position - enemyStatemachine.transform.position;
        faceTargetPos.y = 0f;
      enemyStatemachine.transform.rotation = Quaternion.LookRotation(faceTargetPos);
    }
}
