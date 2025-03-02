using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField] private bool hideHealthBarWhenFull = true;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [Header("Components")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject character;

    private void Start()
    {
        // Set current health
        currentHealth = maxHealth;
        
        // Set healthBar values
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        
        // Hide health bar when hp is full if option is toggled
        healthBar.gameObject.SetActive(!hideHealthBarWhenFull);
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
            // Hide health bar when hp is full if option is toggled
            healthBar.gameObject.SetActive(!hideHealthBarWhenFull);
        }
        else
        {
            healthBar.gameObject.SetActive(true);
        }
        
        // Check if health is 0 
        if (currentHealth <= 0)
        {
            // Character has no more health
            Destroy(character);
        }
    }
}
