using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Health
    public int startingHealth = 100;
    public int currentHealth;
    


    BoxCollider2D boxCollider;

    bool isDead;

    void Awake()
    {
        currentHealth = startingHealth;
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame

    void Update()
    {
        
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Death();
        }

        
    }

    void Death()
    {
        isDead = true;

        

        Destroy(gameObject);
    }
}
