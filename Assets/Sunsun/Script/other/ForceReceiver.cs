using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{

    [SerializeField]   CharacterController characterController;
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
     }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
