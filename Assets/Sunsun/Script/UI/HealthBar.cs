using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NIMA.UI
{
    public class HealthBar : MonoBehaviour
    {
        HealthSystem healthSystem;
        public Slider healthSlider;
        public void Setup(HealthSystem healthSystem)
        {
            this.healthSystem = healthSystem;
            healthSystem.onHealthChange += HealthSystem_OnHealthChanged;
        }

        void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
        {
            //改變healthbar長度
            transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPerscent(), 1);
        }       
    }
}
