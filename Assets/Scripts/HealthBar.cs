using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public int healthValue;

    public void FixedUpdate()
    {

    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        healthValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if(healthValue <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        // Implement death functionality
    }
}
