using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        slider.maxValue = 100;
        slider.value = 100;
    }
    public void SetHealth(float health)
    {
        slider.value = health; 
    }
}
