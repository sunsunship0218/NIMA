using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManagement : MonoBehaviour
{
    [SerializeField] ParticleSystem SlashParticlePrefab;
    [SerializeField] Transform Hitposition;
    public void PlayHitParticle(Vector3 hitposition)
    {
        //�b���w��m/�I����m����S��
        ParticleSystem hitEffect = Instantiate(SlashParticlePrefab, hitposition, Quaternion.identity);
        hitEffect.Play();
        //���񧹫�R���إߪ����,�קK�@���s�b��scene
        Destroy(hitEffect.gameObject, hitEffect.main.duration);
    }
}
