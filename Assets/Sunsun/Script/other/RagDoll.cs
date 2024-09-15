using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;
    Collider[] allColi;
    Rigidbody[] allRigi; 
    void Start()
    {

        // �]�t inActive��Gameobject
        allColi = GetComponentsInChildren<Collider>(true);
        allRigi = GetComponentsInChildren<Rigidbody>(true);
        toogleRagdoll(false);
    }
    //�|�bdeadstate�I�s
    public void toogleRagdoll(bool isRagdoll)
    {
        // is ragdoll,����colider
        foreach(var col in allColi)
        {
            if (col.gameObject.CompareTag("RagDoll"))
            {
                col.enabled = isRagdoll;
            }
        }

        //is ragdoll,�Q���O�v�T
        foreach (var rigi in allRigi)
        {
            if (rigi.gameObject.CompareTag("RagDoll"))
            {
                rigi.isKinematic = !isRagdoll;
                rigi.useGravity = isRagdoll;
            }
        }
       characterController.enabled =! isRagdoll;
       animator.enabled = !isRagdoll;
    }
}
