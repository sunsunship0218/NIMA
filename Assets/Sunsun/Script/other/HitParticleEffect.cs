using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem hityParticlePrefab;
    public void PlayHitParticle(Vector3 hitposition)
    {
        ParticleSystem hitEffect = Instantiate(hityParticlePrefab, hitposition, Quaternion.identity);
        hitEffect.Play();

    }
}
