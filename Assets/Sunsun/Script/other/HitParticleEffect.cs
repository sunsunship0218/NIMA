using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem hityParticlePrefab;
    public void PlayHitParticle(Vector3 hitposition)
    {
        //在指定位置/碰撞位置撥放特效
        ParticleSystem hitEffect = Instantiate(hityParticlePrefab, hitposition, Quaternion.identity);
        hitEffect.Play();
        //撥放完後摧毀建立的實例,避免一直存在於scene
        Destroy(hitEffect.gameObject, hitEffect.main.duration);
    }
}
