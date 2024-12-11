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
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(Camera);
        Tiger.OnTigerDestroyed += HandleTigerDeath;
    }
    void OnDestroy()
    {
        // 確保事件被正確解除訂閱，防止內存洩漏
        Tiger.OnTigerDestroyed -= HandleTigerDeath;
    }
    void HandleTigerDeath()
    {
        StartCoroutine(PlayAnimationAndSwitchScene());
    }
    IEnumerator PlayAnimationAndSwitchScene()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("Level_Drgon");
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
