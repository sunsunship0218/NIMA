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
<<<<<<< Updated upstream
            triggerBox = weapon.GetComponent<BoxCollider>();
          
=======
            triggerBox = weapon.GetComponent<BoxCollider>();        
>>>>>>> Stashed changes
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {              
<<<<<<< Updated upstream
                Debug.Log(gameManager.npc1HealthSystem.GetHealth());
                gameManager.npc1HealthSystem.Damage(damage);
=======
                Debug.Log("ATK");
                gameManager.npc1HealthSystem.Damage(damage);
                Debug.Log(gameManager.npc1HealthSystem.GetHealth());
>>>>>>> Stashed changes

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