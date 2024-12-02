using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticleEffect : MonoBehaviour
{
    [SerializeField] Effect_ObjectPool pool;
    public void PlayHitParticle(Vector3 hitposition)
    {
        //在指定位置/碰撞位置撥放特效
        pool.ReuseParticlePlay( hitposition, Quaternion.identity);
    }
}
