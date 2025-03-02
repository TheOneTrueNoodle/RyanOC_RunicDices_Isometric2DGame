using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HurtBox : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MoveCharacter moveCharacter;
    [SerializeField] private Light2D damageFlash;

    private const float damageFlashTime = 0.2f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<HitBox>(out HitBox hitBox))
        {
            // Ensure attack can hit this target
            if(hitBox.originHurtBox == this){return;}
            
            // Take damage
            healthComponent.UpdateHealth(-hitBox.damage);
            
            // Get knock back direction
            
            Vector2 dir = transform.position - (hitBox.useHitBoxForKnockback ? hitBox.transform.position : hitBox.originHurtBox.transform.position);
            dir.Normalize();
            
            dir *= hitBox.knockbackStrength;
            
            // Check if we need to destroy hitbox
            if (hitBox.destroyOnHit)
            {
                Destroy(hitBox.destroyOnHitObj);
            }
            
            // TODO Change this from a Coroutine as it would be intensive with large enemy groups and aoe damage.
            // Animation effects 
            StartCoroutine(DamageReaction(dir));
        }
    }

    IEnumerator DamageReaction(Vector2 dir)
    {
        // Set important variables
        float flashTimer = damageFlashTime;
        damageFlash.enabled = true;
        moveCharacter.canMove = false;
        
        // Apply knock back TODO Fix knockback
        rb.AddForce(dir, ForceMode2D.Impulse); 

        while (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;
            yield return null;
        }
            
        damageFlash.enabled = false;
        moveCharacter.canMove = true;
    }
}
