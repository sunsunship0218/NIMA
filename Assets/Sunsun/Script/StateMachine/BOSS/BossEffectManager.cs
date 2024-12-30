using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffectManager : MonoBehaviour
{
  [SerializeField]  SwordBullet [] swordBullets;
   public void PlayBulletEffect()
    {
        foreach (var bullet in swordBullets)
        {
            bullet.Shoot();
        }
    }
}
