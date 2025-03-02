using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [SerializeField] private Slider healthBar;

    private void Start()
    {
        // Set current health
        currentHealth = maxHealth;
        
        // Set healthBar values
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        
        // Hide health bar when hp is full
        healthBar.gameObject.SetActive(false);
    }

    public void UpdateHealth(int modifier)
    {
        // Apply new health modifier
        currentHealth += modifier;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        // Update Health Bar
        healthBar.value = currentHealth;
        
        // Check if health is full
        if (currentHealth == maxHealth)
        {
            // Hide health bar when hp is full
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            healthBar.gameObject.SetActive(true);
        }
        
        // Check if health is 0 
        if (currentHealth <= 0)
        {
            // Character has no more health
            Debug.Log("Character is dead");
        }
    }
}
