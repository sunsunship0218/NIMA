using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] Collider playerColi;
    [SerializeField] GameManager gameManager;
    List<Collider> alreadyColiWith =new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        //Á×§K­«½Æ¸I¼²¦©¦å
        if (alreadyColiWith.Contains(other)) { return; }

        alreadyColiWith.Add(other);
        if(other == playerColi)
        {
            gameManager.playerHealthSystem.Damage(10);
            gameManager.playerHealthSystem.GetHealth();
        }
    }
}
