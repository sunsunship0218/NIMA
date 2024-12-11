using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffect : MonoBehaviour
{
    [SerializeField] Effect_ObjectPool pool;
    [SerializeField] Collider Sole_coli;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Ground")
        {
            pool.ReuseParticlePlay(Sole_coli.transform.position, Quaternion.identity);
        }

    }
}
