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
            EnemyStateMachine enemyStateMachine = other.GetComponent<EnemyStateMachine>();
            playerHealth.healthSystem.PostureIncrese(posture);
       

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

}
