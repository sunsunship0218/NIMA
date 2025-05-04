using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//接收實際的狀態機的實作
public class PlayerStateMachine : StateMachine
{
    //接收玩家輸入傳送至Mono的窗口
    [field: SerializeField]
    public playerInputHandler playerInputHandler { get; private set; }

    [field: SerializeField]
    public CharacterController characterController { get; private set; }

    //移動相關的參數
    [field: SerializeField]
    public float freeLookMoveSpeed{ get; private set; }
    [field: SerializeField]
    public float LockonMoveSpeed { get; private set; }
    [field: SerializeField]
    public ForceReceiver forceReceiver { get; private set; }
    [field: SerializeField]
    public float moveRotationDamping { get; private set; }
    //躲避的參數
    [field: SerializeField]
    public float DodgeTime { get; private set; }
    [field: SerializeField]
    public float DodgeDistance { get; private set; }

    //攻擊combo
    [field: SerializeField]
    public Attack[ ] Attacks { get; private set; }
    //攻擊傷害
    [field: SerializeField]
    public WeaponDamage RightweaponDamage { get; private set; }
    [field: SerializeField]
    public WeaponDamage LeftweaponDamage { get; private set; }
    [field: SerializeField]
    public BlockPostureHandler blockPostureHandler{ get; private set; }
    //其他componment
    [field: SerializeField]
    public Animator animator { get; private set; }

    [field: SerializeField]
    public Targeter targeter { get; private set; }
    //處存面向敵人的清單
    [SerializeField]
   public List<GameObject> EnemyList;

    [field: SerializeField]
    public  PlayerHealth playerHealth { get; private set; }

    [field: SerializeField]
    public RagDoll ragDoll { get; private set; }
   

    public Transform mainCameraTransform { get; private set; }
    void Start()
     {
        
        mainCameraTransform = Camera.main.transform;
        //this 就是現在的PlayerStateMachine實例
        SwitchState(new PlayerFreeLookState(this));
        CollectEnemies();
    }

    //事件啟用訂閱
    private void OnEnable()
    {
        if (playerHealth != null && playerHealth.healthSystem != null)
        {
            playerHealth.healthSystem.OnTakeDamage += HandleTakeDamage;
            playerHealth.healthSystem.OnStagger += HandlePostureFull;
            playerHealth.healthSystem.OnDie += HealthSystem_OnDie;
            Debug.Log("Subscribed to health system events");
        }
        else
        {
            Debug.LogError("playerHealth or playerHealth.healthSystem is null in OnEnable");
        }
    }
//事件禁用訂閱
    private void OnDisable()
    {
        playerHealth.healthSystem.OnTakeDamage -= HandleTakeDamage;
        playerHealth.healthSystem.OnDie -= HealthSystem_OnDie;
        playerHealth.healthSystem.OnDie -= HealthSystem_OnDie;
    }

    //Damage事件訂閱方法
     void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }
    //格擋值滿了訂閱
    void HandlePostureFull()
    {
        //清空格擋值
        playerHealth.healthSystem.SetPostureDefault();
        playerHealth.healthSystem.Damage(40);
        //進入impact   
        SwitchState(new PlayerImpactState(this));
        Debug.Log("FULL To IMPACT");
    }
    //死亡訂閱方法
    void HealthSystem_OnDie()
    {
       
        SwitchState(new PlayerDeadState(this));
    }
    //格黨滿了的訂閱方法
    void HealthSystem_OnStagger()
    {
        SwitchState(new PlayerImpactState(this));
    }
    //敵人的List
    void CollectEnemies()
    {
        EnemyList.Clear(); // 確保清空列表
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemyObjects)
        {
           EnemyList.Add(enemy);
        }
    }
    public bool IsDodgeAnimationPlaying()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("DodgeBlendtree") && stateInfo.normalizedTime < 1f;
    }



}
