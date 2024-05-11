using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                        : base(_enemy, _agent, _anim, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
        anim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Patrol(npc, agent, anim, player);
            Debug.Log("Start Partrol");
            stage = EVENT.EXIT;//這句沒有被觸發
            Debug.Log(stage.ToString());
        }
        //0.1的機率切換狀態
        else if (Random.Range(0, 100) < 10)
        {
            nextState = new Patrol(npc, agent, anim, player);
            //現在的狀態準備退出
            stage = EVENT.EXIT;
        }

        //enum切成exit後又馬上變回去update了
        //base.Update();

    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
