using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
        Debug.Log("Hello:");
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
