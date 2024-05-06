using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        SLEEP
    };
    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    };
    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected NavMeshAgent agent;
    //ourself class//指標
    protected State nextState;
    float visDistance = 10f;
    float visAngel = 30f;
    float attackDis = 7f;

    public State(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _enemy;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;

    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public  virtual void Exit() { stage = EVENT.EXIT; }

    public State process()
    {
        if (stage == EVENT.ENTER) { Enter(); }

        if (stage == EVENT.UPDATE) { Update(); }

        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        //回傳現在狀態
        return this;
    }
}

public class Idle : State
{
    public Idle(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                        :base (_enemy, _agent, _anim, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        anim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        if (Random.Range(0, 100) < 10)
        {
            nextState = new Patrol(npc, agent, anim,player);
            stage = EVENT.EXIT;
        }       
        base.Update();
    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}

public  class Patrol : State
{
    int currentIndex = -1;
    public Patrol(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                        : base(_enemy, _agent, _anim, _player)
    {
        name = STATE.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
    }
    public override void Enter()
    {
        currentIndex = 0;
        anim.SetTrigger("isWalking");
        base.Enter();
    }
    public override void Update()
    {
        if (agent.remainingDistance<1)
        {
            if(currentIndex >= GameEnvironment.Singleton.Checkpoints.Count-1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);
        }    
        //base.Update();
    }

    public override void Exit()
    {
        anim.ResetTrigger("isWalking");
        base.Exit();
    }

}