using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    public int maxHealth = 100;
    private ScoreManager scoreManager; // Remove public, it will be assigned using FindObjectOfType.
    public HealthBar healthBar;
    private Animator anim;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>(); // Find the ScoreManager component in the scene.
        anim = GetComponent<Animator>();
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(health, maxHealth);
        }
    }

    public void SetCharacterHealth(int newMaxHealth, int newCurrentHealth)
    {
        maxHealth = newMaxHealth;
        health = newCurrentHealth;
        UpdateHealthBar();
    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthBar();

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            Die();
        }
    }

    private void Die()
    {
        
        int enemyScoreValue = GetComponent<Enemy>().ScoreValue;

        if (scoreManager != null)
        {
            scoreManager.AddScore(enemyScoreValue);
        }
        Destroy(gameObject, 0.3f);
    }
}
