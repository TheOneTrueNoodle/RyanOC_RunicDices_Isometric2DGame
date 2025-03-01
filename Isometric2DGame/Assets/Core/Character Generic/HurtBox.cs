using System;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<HitBox>(out HitBox hitBox))
        {
            // Ensure attack can hit htis target
            if(hitBox.originHurtBox == this){return;}
            
            // Take damage
            Debug.Log("Take " + hitBox.damage + " damage");
            
            // Apply knock back based on the position and knock back force of the attacker
            
            
            // Animation effects
        }
    }
}
