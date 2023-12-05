using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color lowColor;
    public Color highColor;
    public Vector3 offset;

    public void SetHealth(int health, int maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth); 
        slider.value = health;
        slider.maxValue = maxHealth;

        Image fillImage = slider.fillRect.GetComponentInChildren<Image>();
        fillImage.color = Color.Lerp(lowColor, highColor, slider.normalizedValue);
    }

    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
