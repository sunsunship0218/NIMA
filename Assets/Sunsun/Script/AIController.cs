using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance =5f;

    private void Update()
    {
        if(DistanceToPlayer( ) < chaseDistance)
        {
            Debug.Log(gameObject.name +"chasing");
        }


    }

    //敵人與玩家的距離
    private float DistanceToPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        return Vector3.Distance(player.transform.position, transform.position);
    }

}
