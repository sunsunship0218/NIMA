using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform target;
    Ray lastRay;
    private void Update()
    {
        lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
        GetComponent<NavMeshAgent>().destination = target.position;

    }

}
