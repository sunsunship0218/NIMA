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

    //virtual void 骨架
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
    public bool CanSeePlayer()
    {
        Vector3 direc = player.transform.position - npc.transform.position;
        float angels =Vector3.Angle(direc,npc.transform.forward);
        if(direc.magnitude <visDistance && angels <visAngel)
        {
            return true;
        }
        return false; 
    }

    public bool CanAttackPlayer()
    {
        Vector3 direc = player.transform.position - npc.transform.position;
        if (direc.magnitude < attackDis)
        {
            return true;
        }
       return false;

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
        //0.1的機率切換狀態
        if (Random.Range(0, 100) < 10)
        {
            nextState = new Patrol(npc, agent, anim,player);
            //現在的狀態準備退出
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
            else if(!CanSeePlayer())
            {
                 nextState =new Patrol(npc,agent,anim,player);
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

public class Attack :State
{
    float rotationspeed = 5f;
    //AudioSource source;
    public Attack(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                     : base(_enemy, _agent, _anim, _player)
    {
        name = STATE.ATTACK;     
       // source =npc.GetComponent<AudioSource>();
    }
    public override void Enter()
    {
        anim.SetTrigger("isAttacking");
        agent.isStopped = true;
        base.Enter();
    }
    public override void Update()
    {
        base.   Update();
    }
    public override void    Exit()
    {
        base.Exit();
    }
}