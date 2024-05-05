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
    //ourself class//����
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
    public void Exit() { stage = EVENT.EXIT; }

    public State process()
    {
        if (stage == EVENT.ENTER) { Enter(); }

        if (stage == EVENT.UPDATE) { Update(); }

        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        //�^�ǲ{�b���A
        return this;
    }
}