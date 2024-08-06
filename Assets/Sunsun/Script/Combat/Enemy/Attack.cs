/*using UnityEngine.AI;
using UnityEngine;

public class Attack : State
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
        //source.Play();
        base.Enter();
    }
    public override void Update()
    {
        Vector3 direc = player.transform.position - npc.transform.position;
        float angel = Vector3.Angle(direc, npc.transform.forward);
        direc.y = 0;
        //§ðÀ»«á­±´Âª±®a
        npc.transform.rotation = Quaternion.Slerp
            (npc.transform.rotation,
            Quaternion.LookRotation(direc),
            Time.deltaTime * rotationspeed
            );
        if (!CanAttackPlayer())
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;

        }
       // base.Update();
    }
    public override void Exit()
    {
        anim.ResetTrigger("isAttacking");
        //source.Stop();
        base.Exit();
    }
}*/