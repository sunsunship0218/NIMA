using NIMA.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



namespace NIMA.control
{
    public class PlayerController : MonoBehaviour
    {      
        void Update()
        {
            InteracWithCombat();
            Raycast();
        }

        void InteracWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRay());
            foreach(RaycastHit hit in hits)
            {
               CombatTarget Target = hit.transform.GetComponent<CombatTarget>();
                if (Target = null) continue;

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    GetComponent<Fight>().Attack(Target);
                }
            }
        }

        void Raycast()
        {
            RaycastHit hit;          
            if (Physics.Raycast(GetRay(),  out hit))
            {
               // Debug.Log(hit.transform.name + "Hit");
              //  Debug.Log("Distance" + hit.distance);
            }
        }
        Ray GetRay()
        {
            return new Ray(transform.position, Vector3.forward);
        }
    }

}
