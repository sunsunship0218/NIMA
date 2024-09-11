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
        //�b�a��/����a��
        if (characterController.isGrounded && verticalVelocity<0f)
        {
            verticalVelocity = Physics.gravity.y*Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity,drag);
        //���h���A���}�Ҩ���
        
        if (agent!=null && impact == Vector3.zero)
        {
            agent.enabled = true;
        }
     }

    public void AddForce(Vector3 force)
    {
        //��m�Q�O�v�T
        impact += force;
        //AI�Q����ɪ����h,��������
        if (agent != null)
        {
            agent.enabled = false;
        }
    }
}
