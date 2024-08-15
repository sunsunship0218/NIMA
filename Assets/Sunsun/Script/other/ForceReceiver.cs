using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{

    [SerializeField]   CharacterController characterController;
    public Vector3 movement => Vector3.up * verticalVelocity;
    float verticalVelocity;
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
        
     }
}
