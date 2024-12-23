using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPostureHandler : MonoBehaviour
{
    float posture;
    List<Collider> alreadyColiWith = new List<Collider>();
    [SerializeField] Collider myColi;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] EnemyHealth enemyHealth;
    private void Awake()
    {

    }

    private void Update()
    {
        if (playerHealth.healthSystem.GetPostureAmount() > 0)
        {
            playerHealth.healthSystem.PostureDecrease(5f * Time.deltaTime);
        }
    }
    private void OnEnable()
    {
        alreadyColiWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == myColi) { return; }
        if (alreadyColiWith.Contains(other)) { return; }
        if (other.tag != "Player" && other.tag != "Enemy") { return; }
        alreadyColiWith.Add(other);

        if (other.tag == "Enemy")
        {
            //Posture在
            EnemyStateMachine enemyStateMachine = other.GetComponent<EnemyStateMachine>();
            playerHealth.healthSystem.PostureIncrese(50);
            Debug.Log(posture);
       

        }
        if (other.tag == "Player")
        {
        
            enemyHealth.healthSystem.PostureIncrese(posture);

        }
    }

    public void SetPosture(float posture)
    {
      this.posture = posture;
    }

    //處理試鍵訂閱

}
