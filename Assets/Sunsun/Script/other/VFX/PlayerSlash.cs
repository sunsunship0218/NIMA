using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    [SerializeField] ParticleSystem combo1_ps;
    [SerializeField] ParticleSystem combo2_ps;
    [SerializeField] ParticleSystem combo3_ps;
    [SerializeField] ParticleSystem combo3_1_ps;
    void Start()
    {
      
    }
    public void PlayCombo1Effect()
    {        
        combo1_ps.Play();
    }
    public void PlayeCombo2Effect()
    {
        combo2_ps.Play();
    }
    public void PlayeCombo3Effect()
    {
        combo3_ps.Play();
    }
    public void PlayeCombo3_1Effect()
    {
        combo3_1_ps.Play();
    }
}
