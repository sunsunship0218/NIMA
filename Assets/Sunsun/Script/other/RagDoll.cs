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

        // 包含 inActive的Gameobject
        allColi = GetComponentsInChildren<Collider>(true);
        allRigi = GetComponentsInChildren<Rigidbody>(true);
        toogleRagdoll(false);
    }
    //會在deadstate呼叫
    public void toogleRagdoll(bool isRagdoll)
    {
        // is ragdoll,關閉colider
        foreach(var col in allColi)
        {
            if (col.gameObject.CompareTag("RagDoll"))
            {
                col.enabled = isRagdoll;
            }
        }

        //is ragdoll,被重力影響
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
