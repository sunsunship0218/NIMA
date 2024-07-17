using UnityEngine.AI;
using UnityEngine;

public class Patrol : State
{
    int currentIndex = -1;
    public Patrol(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                        : base(_enemy, _agent, _anim, _player)
    {
        name = STATE.PATROL;
        agent.speed = 3;
        agent.isStopped = false;
    }
    public override void Enter()
    {
       // Debug.Log("Entering Patrol State");
        float lastDist = Mathf.Infinity;
        for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++)
        {
            GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];
            float dist = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if (dist < lastDist)
            {
                currentIndex = i - 1;
                lastDist = dist;
            }
        }
        currentIndex = 0;
        anim.SetTrigger("isWalking");
       // Debug.Log("Number of Checkpoints: " + GameEnvironment.Singleton.Checkpoints.Count);
        base.Enter();
    }
    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);
        }
        if (CanSeePlayer())
        {
            nextState = new Chase(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if (!CanSeePlayer())
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        //base.Update();
    }

    public override void Exit()
    {
      //  Debug.Log("Exiting Patrol State");
        anim.ResetTrigger("isWalking");
        base.Exit();
    }

}