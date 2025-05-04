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
  //�q�\�U�ӼĤH���`�ƥ�,�ĤH���ƥ�g�b�U�۪�l�ƪ����A��
        Tiger.OnTigerDestroyed += HandleTigerDeath;
    }
    void OnDestroy()
    {
        // �T�O�ƥ�Q���T�Ѱ��q�\�A����s���|
        Tiger.OnTigerDestroyed -= HandleTigerDeath;
    }
    void HandleTigerDeath()
    {
        //�令�b�P�@��scene,�γ���,�ɤ���scene
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
