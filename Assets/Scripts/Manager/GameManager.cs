using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int health = 5;
    public int damage = 1;
    public float fireRate = 0.2f;
    public float reloadTime = 1f;
    public int bullets = 30;
    
    public static GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        else
        {
             Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
