using UnityEngine.AI;
using UnityEngine;

public class Chase : State
{
    public Chase(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                        : base(_enemy, _agent, _anim, _player)
    {
        name = STATE.CHASE;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
       
        anim.SetTrigger("isChasing");
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.transform.position);
        //檢查目前有沒有AI 有效 能走的路
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                nextState = new Patrol(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
        base.Update();
    }

    public override void Exit()
    {
        anim.ResetTrigger("isChasing");
        base.Exit();
    }
}