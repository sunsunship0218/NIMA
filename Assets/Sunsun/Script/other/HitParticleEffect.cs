using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticleEffect : MonoBehaviour
{
    [SerializeField] Effect_ObjectPool pool;
    public void PlayHitParticle(Vector3 hitposition)
    {
        //�b���w��m/�I����m����S��
        pool.ReuseParticlePlay( hitposition, Quaternion.identity);
    }
}
