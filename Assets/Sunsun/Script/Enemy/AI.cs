using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    State currentState;
    private void Start()
    {
        agent=this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        currentState = new Patrol(gameObject, agent, anim, player);
    }

    private void Update()
    {
        currentState = currentState.process();
    }
}
