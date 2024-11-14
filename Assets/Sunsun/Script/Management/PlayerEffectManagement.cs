using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManagement : MonoBehaviour
{
    [SerializeField] ParticleSystem SlashParticlePrefab;
    [SerializeField] Transform Hitposition;
    public void PlayHitParticle(Vector3 hitposition)
    {
        //在指定位置/碰撞位置撥放特效
        ParticleSystem hitEffect = Instantiate(SlashParticlePrefab, hitposition, Quaternion.identity);
        hitEffect.Play();
        //撥放完後摧毀建立的實例,避免一直存在於scene
        Destroy(hitEffect.gameObject, hitEffect.main.duration);
    }
}
