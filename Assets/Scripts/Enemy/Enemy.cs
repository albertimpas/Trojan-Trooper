using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int scoreValue = 0;
    [SerializeField] private EnemyData data;
    
    private GameObject player;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        SetEnemyValues();
    }

    // Update is called once per frame
    void Update()
    {
        Swarm();
    }

    private void SetEnemyValues()
    {
        Health enemyHealth = GetComponent<Health>();

        if (enemyHealth != null)
        {
            enemyHealth.SetCharacterHealth(data.hp, data.hp);
        }

        damage = data.damage;
        speed = data.speed;
        scoreValue = data.score;
    }


    private void Swarm()
    {
        // Calculate the direction to the player.
        Vector2 moveDirection = (player.transform.position - transform.position).normalized;

        // Check and apply flipping only when the direction changes.
        if ((isFacingRight && moveDirection.x < 0) || (!isFacingRight && moveDirection.x > 0))
        {
            Flip();
        }

        // Move the enemy.
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        // Flip the enemy's scale to change its direction.
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GetComponent<Health>().Damage(10000);
        }
    }

    public int ScoreValue
    {
        get { return scoreValue; }
    }
}
