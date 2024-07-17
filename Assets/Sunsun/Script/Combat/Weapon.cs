using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NIMA.Combat
{
    public class Weapon : MonoBehaviour
    {
        public float damage;
      [SerializeField]  GameObject weapon;
        BoxCollider triggerBox;

        private void Awake()
        {
            triggerBox = weapon.GetComponent<BoxCollider>();
        }
        void OnTriggerEnter(Collider other)
         {
            
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