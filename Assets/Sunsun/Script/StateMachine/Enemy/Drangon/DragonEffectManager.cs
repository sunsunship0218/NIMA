using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEffectManager : MonoBehaviour
{
    [SerializeField] AudioClip ATK1_clip;
    [SerializeField] AudioClip ATK2_clip;
    [SerializeField] AudioClip ATK3_clip;
    [SerializeField] AudioClip ATK4_clip;
    [SerializeField] AudioClip ATK5_clip;
    [SerializeField] AudioClip ATK6_clip;
    [SerializeField] AudioClip ATK7_clip;

    [SerializeField]ParticleSystem weaponEffect1;
    [SerializeField] ParticleSystem weaponEffect2;
    [SerializeField] ParticleSystem weaponEffect3;

    [SerializeField] AudioSource ATK_audio;
    public void Play_ATK1()
    {
        ATK_audio.clip = ATK1_clip;
        ATK_audio.Play();
    }
    public void Play_ATK2()
    {
        ATK_audio.clip = ATK2_clip;
        ATK_audio.Play();
    }
    public void Play_ATK3()
    {
        ATK_audio.clip = ATK3_clip;
        ATK_audio.Play();
    }
    public void Play_ATK4()
    {
        ATK_audio.clip = ATK4_clip;
        ATK_audio.Play();
    }
    public void Play_ATK5()
    {
        ATK_audio.clip = ATK5_clip;
        ATK_audio.Play();
    }
    public void Play_ATK6()
    {
        ATK_audio.clip = ATK6_clip;
        ATK_audio.Play();
    }
    public void Play_ATK7()
    {
        ATK_audio.clip = ATK7_clip;
        ATK_audio.Play();
    }

    //1
    public  void PlayEffect1()
    {
     
        weaponEffect1.Play();
    }
    public void StopEffect1()
    {
      weaponEffect1.Stop();

    }
    //2
    public void PlayEffect2()
    {

        weaponEffect2.Play();
    }
    public void StopEffect2()
    {
        weaponEffect2.Stop();

    }
    //3
    public void PlayEffect3()
    {

        weaponEffect3.Play();
    }
    public void StopEffect3()
    {
        weaponEffect3.Stop();

    }
    //4
   
}
