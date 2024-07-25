using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NIMA.UI
{
    public class HealthBar : MonoBehaviour
    {

        public Slider PlayerHealthSlider;
        public Slider Npc1HealthSlider;

        //�����l�̤j��
        public void SetSliderMax(float amount)
        {
            PlayerHealthSlider.maxValue = amount;
            SetSlider(amount);
        }
        //���ܦ����
        public void SetSlider(float amount)
        {
            PlayerHealthSlider.value = amount;
        }
     
     
    }
}
