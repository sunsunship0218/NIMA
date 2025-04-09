using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [Header("ATK_VFX")]
    [SerializeField] ParticleSystem combo1_ps;
    [SerializeField] ParticleSystem combo2_ps;
    [SerializeField] ParticleSystem combo3_ps;
    [SerializeField] ParticleSystem combo4_ps;
    [SerializeField] ParticleSystem combo5_ps;
    [Header("ATK_SFX")]
    [SerializeField] AudioClip combo1Clip;
    [SerializeField] AudioClip combo2Clip;
    [SerializeField] AudioClip combo3Clip;
    [SerializeField] AudioClip combo4Clip;
    [SerializeField] AudioClip combo5Clip;
    [Header("Dodge_SFX")]
    [SerializeField] AudioClip dodgeClip;
    [SerializeField] AudioSource audiosource;
    [Header("ATK_Voice")]
    [SerializeField]  AudioClip[] AttackVoiceClips;
    [Header("Hurt_Voice")]
    [SerializeField] AudioClip[]HurtClips;

    [SerializeField] AudioSource VoiceSource;
    [SerializeField] AudioSource HurtSource;
    public void PlayCombo1Effect()
    {
        audiosource.clip = combo1Clip;
        combo1_ps.Play();
        audiosource.Play();
    }
    public void PlayeCombo2Effect()
    {
        audiosource.clip = combo2Clip;
        combo2_ps.Play();
        audiosource.Play();
    }
    public void PlayeCombo3Effect()
    {
        audiosource.clip = combo3Clip;
        combo3_ps.Play();
        audiosource.Play();
    }
    public void PlayeCombo4Effect()
    {
        audiosource.clip = combo4Clip;
        combo4_ps.Play();
        audiosource.Play();
    }
    public void PlayeCombo5Effect()
    {
        audiosource.clip = combo5Clip;
        combo5_ps.Play();
        audiosource.Play();
    }

    public void PlayerDodgeEffect()
    {
        audiosource.clip = dodgeClip;
        audiosource.Play();
    }
    public void PlayRandomAttackSound()
    {
        // 在 gruntClips 中隨機選一個
        int randomIndex = Random.Range(0, AttackVoiceClips.Length);
       VoiceSource.clip = AttackVoiceClips[randomIndex];
        VoiceSource.Play();
    }

    public void PlayRandomHurtSound()
    {
        // 在 gruntClips 中隨機選一個
        int randomIndex = Random.Range(0, HurtClips.Length);
        HurtSource.clip =HurtClips[randomIndex];
        HurtSource.Play();
    }
}
