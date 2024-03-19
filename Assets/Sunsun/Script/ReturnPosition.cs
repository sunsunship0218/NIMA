using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPosition : MonoBehaviour
{
    public Transform currentPosition;
    public void OnTriggerEnter(Collider other)
    {
        //Trigger return player's position
        other.transform.position = currentPosition.position;
        
    }
}
