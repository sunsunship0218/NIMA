using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Defend : State
{
    public Defend(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                   : base(_enemy, _agent, _anim, _player)
    {
        name = STATE.Defend;
     
    }

    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
