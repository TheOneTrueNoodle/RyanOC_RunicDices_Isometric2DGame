using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private HitBox projectileHitBox;
    [SerializeField] private Rigidbody2D rb;
    
    private Vector2 dir;
    private float speed;
    
    private void Update()
    {
        if(rb is null){return;}
        rb.linearVelocity = dir * speed;
    }

    public void SetupProjectile(Vector2 direction, float projectileSpeed, int projectileDamage, HurtBox origin)
    {
        projectileHitBox.originHurtBox = origin;
        
        projectileHitBox.Setup(origin, projectileDamage);
        
        dir = direction;
        dir.Normalize();
        speed = projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
