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
        Defend
    };
    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    };
    //state
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
    float attackDis = 4f;

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
        Debug.Log("Processing State: " + name.ToString() + " at stage " + stage.ToString());

        if (stage == EVENT.ENTER) { Enter(); }

        if (stage == EVENT.UPDATE) { Update(); }

        if (stage == EVENT.EXIT)
        {
            Exit();
            Debug.Log("Exiting State: " + name.ToString());
            return nextState;
        }
        //回傳現在狀態
        return this;
    }
    public bool CanSeePlayer()
    {
        Vector3 direc = player.transform.position - npc.transform.position;
        float angels =Vector3.Angle(direc,npc.transform.forward);
        //Debug.Log("Angels: " + angels + "  positionDifference " + direc);
        if(direc.magnitude <visDistance)//&& angels <visAngel
        {
            Debug.Log("Can see player True");
            return true;
        }
        else
        {
            Debug.Log("Can see player  False");
            return false;
        }
        
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
