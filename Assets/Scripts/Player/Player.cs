using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 600;
    public GameObject bulletPrefab;
    public Transform shotSpawner;
    public float damageTime = 1f;

    private Animator anim;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool jump;
    private bool onGround = false;
    private Transform groundCheck;
    private float hForce = 0;
    private bool reloading;
    private float fireRate = 0.5f;
    private float nextFire;
    private bool tookDamage = false;

    private int bullets;
    private float reloadTime;
    private int health;
    private int maxHealth;
    private bool isDead = false;

    GameManager gameManager;
    public GameObject gameOverPanel;
    public ScoreManager scoreManager;

    [SerializeField] private AudioSource shootSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>();

        gameManager = GameManager.gameManager;
        SetPlayerStatus();
        health = maxHealth;

        UpdateBulletsUI();
        UpdateHealthUI();
    }

    void Update()
    {
        if (!isDead)
        {
            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

            if (onGround)
            {
                anim.SetBool("Jump", false);
            }

            if (jump)
            {
                if (rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
            }
        }
    }

    private void FixedUpdate()
{
    if (!isDead)
    {
        float inputHForce = Input.GetAxisRaw("Horizontal");

        // Only use keyboard input if no UI button input is active
        if (inputHForce == 0)
        {
            inputHForce = hForce; // Use the UI button input
        }

        anim.SetFloat("Speed", Mathf.Abs(inputHForce));

        rb.velocity = new Vector2(inputHForce * speed, rb.velocity.y);

        if (inputHForce > 0 && !facingRight)
        {
            Flip();
        }
        else if (inputHForce < 0 && facingRight)
        {
            Flip();
        }

        if (jump)
        {
            anim.SetBool("Jump", true);
            jump = false;
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
}


    public void Jump()
    {
        Debug.Log("Jump button pressed.");
        if (onGround && !reloading)
        {
            jump = true;
        }
    }

    public void Fire()
    {
        
        if (Time.time > nextFire && bullets > 0)
        {
            nextFire = Time.time + fireRate;
            anim.SetTrigger("Shoot");
            shootSoundEffect.Play();
            GameObject tempBullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
            if (!facingRight)
            {
                tempBullet.transform.eulerAngles = new Vector3(0, 0, 180);
            }

            bullets--;
            UpdateBulletsUI();
        }
        else if (bullets <= 0 && onGround)
        {
            StartCoroutine(Reloading());
        }
    }

    public void Reload()
    {
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        reloading = true;
        anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        bullets = gameManager.bullets;
        reloading = false;
        anim.SetBool("Reloading", false);
        UpdateBulletsUI();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void SetPlayerStatus()
    {
        fireRate = gameManager.fireRate;
        bullets = gameManager.bullets;
        reloadTime = gameManager.reloadTime;
        maxHealth = gameManager.health;
    }

    void UpdateBulletsUI()
    {
        FindObjectOfType<UIManager>().UpdateBulletsUI(bullets);
    }

    void UpdateHealthUI()
    {
        FindObjectOfType<UIManager>().UpdateHealthUI(health);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !tookDamage)
        {
            StartCoroutine(TookDamage());
        }
    }

    IEnumerator TookDamage()
    {
        tookDamage = true;
        health--;
        UpdateHealthUI();
        if (health <= 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
            ReloadScene();
            
        }
        else
        {
            Physics2D.IgnoreLayerCollision(9, 10);
            for (float i = 0; i < damageTime; i += 0.2f)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.1f);
                GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
            Physics2D.IgnoreLayerCollision(9, 10, false);
            tookDamage = false;
        }
    }

    void ReloadScene()
    {
        Time.timeScale = 0f; // Unpause the game.
        scoreManager.UpdateHighestScore();
        gameOverPanel.SetActive(true);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartMovingLeft()
    {
        hForce = -1f;
    }

    public void StopMovingLeft()
    {
        if (hForce < 0)
        {
            hForce = 0f;
        }
    }

    public void StartMovingRight()
    {
        hForce = 1f;
    }

    public void StopMovingRight()
    {
        if (hForce > 0)
        {
            hForce = 0f;
        }
    }
}
