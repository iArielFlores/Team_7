using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;  // The amount of damage this bullet deals
    public float bulletLifetime = 5f;  // The time after which the bullet is destroyed, even if it hasn't hit anything

    void Start()
    {
        // Destroy the bullet after a certain time, regardless of collision
        Destroy(gameObject, bulletLifetime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the bullet hits something with a collider tagged as "Enemy"
        if (collider.CompareTag("Enemy"))
        {
            // Get the EnemyHealth component on the object the bullet collided with
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exists, deal damage
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, collider.transform.position); // Use collider's transform.position for hit point
            }

            // Destroy the bullet after hitting an enemy
            Destroy(gameObject);
        }
        else
        {
            // Destroy the bullet if it hits something else
            Destroy(gameObject);
        }
    }
}
