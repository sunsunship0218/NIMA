using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NIMA.Combat
{
    public class Weapon : MonoBehaviour
    {
     public  float damage;
       [SerializeField]  GameObject weapon;
        BoxCollider triggerBox;
       public GameManager gameManager;

        private void Awake()
        {

            triggerBox = weapon.GetComponent<BoxCollider>();

        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {              
                //´î¤Öªº¬Oplayer£x bug
                Debug.Log(gameManager.npc1HealthSystem.GetHealth());
                gameManager.npc1HealthSystem.Damage(damage);
            }
        }

        public void EnableTriggerBox()
        {
            triggerBox.enabled = true;
        }
        public void DisableTriggerBox()
        {
            triggerBox.enabled = false;
        }

    }
}