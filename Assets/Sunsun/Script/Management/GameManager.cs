using NIMA.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HealthSystem playerHealthSystem;
    public HealthSystem npc1HealthSystem;

     public  HealthBar PlayerHealthBar;
    public HealthBar npc1HealthBar;
    public Player player;
    public NPC npc1;

     void Awake()
     {
        //Initial
        player =new Player();
        npc1 = new NPC();

        playerHealthSystem = new HealthSystem(player.playerHP);
        npc1HealthSystem = new HealthSystem(npc1.enemyHP);

        Debug.Log(playerHealthSystem.GetHealth());
        Debug.Log(npc1HealthSystem.GetHealth());
    }
     void Start()
     {
        
        InitialHPSystem();
       
    }

    void InitialHPSystem()
    {
        //血條訂閱血量變化
        playerHealthSystem.onHealthChange += PlayerHealthBar__onHealthChange;
        npc1HealthSystem.onHealthChange += npc1HealthBar__onHealthChange;
        Debug.Log("HEalth : " + playerHealthSystem.GetHealth());

        //初始化玩家血條
        PlayerHealthBar.SetPlayerSliderMax(playerHealthSystem.GetHealth());
        PlayerHealthBar.SetPlayerSlider(playerHealthSystem.GetHealth());

        //初始化npc1血條
        npc1HealthBar.SetNpc1SliderMax(npc1HealthSystem.GetHealth());
        npc1HealthBar.SetNpc1Slider(npc1HealthSystem.GetHealth());
    }
    //根據血量變化事件扣血
    void PlayerHealthBar__onHealthChange(object sender, System.EventArgs e)
    {
        PlayerHealthBar.SetPlayerSlider(playerHealthSystem.ReturnHealth());
    }

    void npc1HealthBar__onHealthChange(object sender, System.EventArgs e)
    {
        npc1HealthBar.SetNpc1Slider(npc1HealthSystem.ReturnHealth());
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
