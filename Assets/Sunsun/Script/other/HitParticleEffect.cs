using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem hityParticlePrefab;
    public void PlayHitParticle(Vector3 hitposition)
    {
        //�b���w��m/�I����m����S��
        ParticleSystem hitEffect = Instantiate(hityParticlePrefab, hitposition, Quaternion.identity);
        hitEffect.Play();
        //���񧹫�R���إߪ����,�קK�@���s�b��scene
        Destroy(hitEffect.gameObject, hitEffect.main.duration);
    }
}
