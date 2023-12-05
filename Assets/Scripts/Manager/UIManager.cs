using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI bulletsText;
    public Slider healthBar;


    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBar();
    }

    public void UpdateBulletsUI(int bullets)
    {
        bulletsText.text = bullets.ToString();
    }

    public void UpdateHealthUI(int health)
    {
        healthBar.value = health;
    }

    public void UpdateHealthBar()
    {
        healthBar.maxValue = GameManager.gameManager.health;
    }
}
