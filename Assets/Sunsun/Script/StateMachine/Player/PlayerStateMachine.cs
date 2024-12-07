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

    [field: SerializeField]
    public  PlayerHealth playerHealth { get; private set; }

    [field: SerializeField]
    public RagDoll ragDoll { get; private set; }
    //UI
    [field: SerializeField]
    public TextMeshProUGUI textMeshProUGUI{ get; private set; }
  //  [SerializeField] TrailRenderer trailRenderer;

    public Transform mainCameraTransform { get; private set; }
    void Start()
     {
        textMeshProUGUI.gameObject.SetActive(false);
        mainCameraTransform = Camera.main.transform;
        //this 就是現在的PlayerStateMachine實例
        SwitchState(new PlayerFreeLookState(this)); 
     }

    //事件啟用訂閱
    private void OnEnable()
    {
        if (playerHealth != null && playerHealth.healthSystem != null)
        {
            playerHealth.healthSystem.OnTakeDamage += HandleTakeDamage;
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
    }

    //Damage事件訂閱方法
     void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }
    //格擋值滿了訂閱
    void HandlePostureFull()
    {

    }
    //死亡訂閱方法
    void HealthSystem_OnDie()
    {
        textMeshProUGUI.gameObject.SetActive(true);
        SwitchState(new PlayerDeadState(this));
    }
    
}
