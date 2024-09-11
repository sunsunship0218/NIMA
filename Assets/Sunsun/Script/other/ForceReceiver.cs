using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.UI;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{

    [SerializeField]   CharacterController characterController;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float drag=0.3f;
    Vector3 impact;
    Vector3 dampingVelocity;
    float verticalVelocity;

    public Vector3 movement =>impact+ Vector3.up * verticalVelocity;

    void Update()
     {
        //在地面/接近地面
        if (characterController.isGrounded && verticalVelocity<0f)
        {
            verticalVelocity = Physics.gravity.y*Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity,drag);
        //擊退完再次開啟巡路
        
        if (agent!=null && impact == Vector3.zero)
        {
            agent.enabled = true;
        }
     }

    public void AddForce(Vector3 force)
    {
        //位置被力影響
        impact += force;
        //AI被打到時的擊退,關閉巡路
        if (agent != null)
        {
            agent.enabled = false;
        }
    }
}
