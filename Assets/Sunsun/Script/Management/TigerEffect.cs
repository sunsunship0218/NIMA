using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TigerEffect : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [Header("Attack1 effect")]
   [SerializeField] VisualEffect SlashVE1;
   [SerializeField] VisualEffect SlashVE2;
  [SerializeField] VisualEffect SlashVE3;
    [SerializeField] AudioClip Attack1Clip;

    [Header("Attack2 effect")]
    [SerializeField] VisualEffect ATK2_Slash1;
    [SerializeField] VisualEffect ATK2_Slash2;
    [SerializeField] VisualEffect ATK2_Slash3;
    [SerializeField] AudioClip Attack2Clip;

    [Header("Attack3 effect")]
    [SerializeField] VisualEffect ATK3_Slash1;
    [SerializeField] VisualEffect ATK3_Slash2;
    [SerializeField] VisualEffect ATK3_Slash3;
    [SerializeField] AudioClip Attack3Clip1;

    [Header("Ground effect")]
    [SerializeField] GameObject tiger_SoleColi;
    [SerializeField] AudioClip GroundClip;
    public void Attack1Play()
    {
        SlashVE1.Play();
        SlashVE2.Play();
        SlashVE3.Play();
        audioSource.clip = Attack1Clip;
        audioSource.Play();
    }
    public void Attack1Stop()
    {
        SlashVE1.Stop();
        SlashVE2.Stop();
        SlashVE3.Stop();
        audioSource.Stop();
    }
    public void Attack2Play()
    {
       ATK2_Slash1.Play();
        ATK2_Slash2.Play();
        ATK2_Slash3.Play();
        audioSource.clip = Attack2Clip;
        audioSource.Play();
    }
    public void Attack2Stop()
    {
        ATK2_Slash1.Stop();
        ATK2_Slash2.Stop();
        ATK2_Slash3.Stop();
        audioSource.Stop();
    }

    public void Attack3Play() 
    {
        ATK3_Slash3.Play();
        ATK3_Slash3.Play();
        ATK3_Slash3.Play();
        audioSource.clip = Attack3Clip1;
        audioSource.Play();
    }
    public void Attack3Stop( ) 
    {
        ATK3_Slash1.Stop();
        ATK3_Slash2.Stop();
        ATK3_Slash3.Stop();
        audioSource.Stop();
    }

    public void GroundedEffectPlay()
    {
        tiger_SoleColi.SetActive( true );
        audioSource.clip = GroundClip;
        audioSource.Play();
    }
    public void GroundedEffectStop()
    {
        tiger_SoleColi.SetActive(false);
        audioSource.Stop();
    }
}
