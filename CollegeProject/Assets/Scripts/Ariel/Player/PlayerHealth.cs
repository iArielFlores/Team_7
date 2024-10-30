using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    bool isDead;
    bool damaged;

    public UnityEvent<int> onHealthChanged;


    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        onHealthChanged.Invoke(currentHealth);

        

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Death()
    {
        isDead = true;
        Destroy(gameObject, 2f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
