using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth(float modifier)
    {
        // Apply new health modifier
        currentHealth += modifier;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        // Check if health is 0 
        if (currentHealth <= 0)
        {
            // Character has no more health
            Debug.Log("Character is dead");
        }
    }
}
