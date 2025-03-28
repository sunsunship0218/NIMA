using NIMA.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject Player;
    public Camera Camera;
    public GameObject UI;
    private void Awake()
    {
        player     = new Player();
        Player = GameObject.Find("Player_");
        UI = GameObject.Find("PlayerCanvas");
    }
    void Start()
    {
     //   DontDestroyOnLoad(this);
    //    DontDestroyOnLoad(Player);
      //  DontDestroyOnLoad(Camera);
  //訂閱各個敵人死亡事件,敵人的事件寫在各自初始化的狀態機
        Tiger.OnTigerDestroyed += HandleTigerDeath;
    }
    void OnDestroy()
    {
        // 確保事件被正確解除訂閱，防止內存洩漏
        Tiger.OnTigerDestroyed -= HandleTigerDeath;
    }
    void HandleTigerDeath()
    {
        //改成在同一個scene,用圍欄,補切換scene
        Debug.Log("level1 finished");

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
