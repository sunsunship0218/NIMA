using NIMA.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    private void Awake()
    {
  player     = new Player();
    }



}

public class Player
{
   public float playerHP = 100;
   public float Light_ATK = 5;
}

public class NPC
{
    public float enemyHP =100;
}
