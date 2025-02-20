using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TigerEffect : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioSource GroundaudioSource;
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

    [SerializeField] VisualEffect ATK3_1Slash1;
    [SerializeField] VisualEffect ATK3_2Slash2;
    [SerializeField] VisualEffect ATK3_3Slash3;

    [SerializeField] AudioClip Attack3Clip1;

    [Header("Attack5 effect")]
    [SerializeField] AudioClip Attack5Clip1;

    [Header("Attack6 effect")]
    [SerializeField] AudioClip Attack6Clip1;
    [SerializeField] AudioClip Attack6Clip3;
    [SerializeField] VisualEffect ATK6_1Slash1;
    [SerializeField] VisualEffect ATK6_2Slash2;
    [SerializeField] VisualEffect ATK6_3Slash3;
    [Header("Attack7 effect")]
    [SerializeField] AudioClip Attack7Clip1;
    [SerializeField] ParticleSystem ATK7_Slash1;

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
    //ATK2
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
    //ATK3
    public void Attack3Play() 
    {
        ATK3_Slash1.Play();
        ATK3_Slash2.Play();
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
    public void Attack3_2Play()
    {
        ATK3_1Slash1.Play();
        ATK3_2Slash2.Play();
        ATK3_3Slash3.Play();
        audioSource.clip = Attack3Clip1;
        audioSource.Play();
    }
    public void Attack3_2Stop()
    {
        ATK3_1Slash1.Stop();
        ATK3_2Slash2.Stop();
        ATK3_3Slash3.Stop();
        audioSource.Stop();
    }
    //ATK4
    public void GroundedEffectPlay()
    {
        tiger_SoleColi.SetActive(true);
        GroundaudioSource.clip = GroundClip;
        GroundaudioSource.Play();
    }
    public void GroundedEffectStop()
    {
        tiger_SoleColi.SetActive(false);
        audioSource.Stop();
    }
    //ATK5
    public void Attack5Play()
    {
        audioSource.clip = Attack5Clip1;
        audioSource.Play();
    }
    public void Attack5Stop() 
    {
        audioSource.Stop();
    }
    //ATK6
    public void Attack6Play() 
    {
        ATK6_1Slash1.Play();
        ATK6_2Slash2.Play();
        ATK6_3Slash3.Play();
        audioSource.clip = Attack6Clip1;
        audioSource.Play();
    }
    public void Attack6Stop()
    {
        ATK6_1Slash1.Stop();
        ATK6_2Slash2.Stop();
        ATK6_3Slash3.Stop();
        audioSource.Stop();
    }
    public void Attack6_2Play()
    {
        audioSource.clip = Attack6Clip1;
        audioSource.Play();
    }
    public void Attack6_2Stop()
    {
        audioSource.Stop();
    }
    public void Attack6_3Play()
    {
        audioSource.clip = Attack6Clip3;
        audioSource.Play();
    }
    public void Attack6_3Stop()
    {
        audioSource.Stop();
    }
    // ATK7
    public void Attack7Play() 
    {
        ATK7_Slash1.Play();
        audioSource.clip = Attack7Clip1;
        audioSource.Play();
    }
    public void Attack7Stop()
    {
        ATK7_Slash1.Stop();
        audioSource.Stop();
    }


}
