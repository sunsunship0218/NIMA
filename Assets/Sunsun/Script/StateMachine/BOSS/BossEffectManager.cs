using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffectManager : MonoBehaviour
{
  [SerializeField]  SwordBullet [] swordBullets;
  [SerializeField] GameObject weaponTrail;
 [SerializeField] ParticleSystem smoke_particle;
 [SerializeField] GameObject ground_Coli;

    [SerializeField] AudioClip smoke_ground;
    [SerializeField] AudioSource smoke_audio;

    [SerializeField] AudioSource attack_audio;
    [SerializeField] AudioSource move_audio;

    [SerializeField] AudioSource ground_audio;
    [SerializeField] AudioClip groundClip;
    [SerializeField] AudioClip roar_clip;
    //ATK1
    [SerializeField] AudioClip ATK1_clip;
    [SerializeField] AudioClip ATK2_clip;
    [SerializeField] AudioClip ATK3_clip;
    [SerializeField] AudioClip ATK5_clip;


    [SerializeField] AudioClip MOVE5_clip;
    public void PlayBulletEffect()
    {
        foreach (var bullet in swordBullets)
        {
            bullet.Shoot();
        }
    }
    public void PlayWeaponTrail()
    {
        weaponTrail.SetActive(true);
    }
    public void StopWeaponTrail()
    {
        weaponTrail?.SetActive(false);
    }

    public void PlaySmoke()
    {
        smoke_audio.clip = smoke_ground;
       smoke_particle.Play();
        smoke_audio.Play();
    }
    public void StopSmoke()
    {
        smoke_audio.Stop();
      smoke_particle.Stop();
    }

    public void PlayGroundEffect()
    {
        ground_audio.clip = groundClip;
        ground_audio.Play();
        ground_Coli.SetActive(true);
    }
    public void StopGroundEffect()
    {
        ground_Coli.SetActive(false);
    }

    public  void TransitionPhase2()
    {
        attack_audio.clip = roar_clip;
        attack_audio.Play();
    }
    public void Play_ATK1()
    {
        attack_audio.clip = ATK1_clip;
        attack_audio.Play();
    }
    public void Play_ATK2()
    {
        attack_audio.clip = ATK2_clip;
        attack_audio.Play();
    }
    public void Play_ATK3()
    {
        attack_audio.clip = ATK3_clip;
        attack_audio.Play();
    }
    public void Play_Move_ATK5()
    {
        move_audio.clip = MOVE5_clip; 
        move_audio.Play();
    }
    public void Play_ATK5()
    {
        attack_audio.clip = ATK5_clip;
        attack_audio.Play();
    }
}
