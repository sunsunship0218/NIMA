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
            damage = gameManager.player.Light_ATK;
            gameManager.npc1HealthSystem.Damage(damage);
        }
        void OnTriggerEnter(Collider other)
        {
            Debug.Log("on coli enter");
            if (other.tag == "Enemy")
            {              
                Debug.Log("ATK");
          
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