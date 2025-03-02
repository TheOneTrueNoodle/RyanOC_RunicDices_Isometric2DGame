using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private Vector2 dir;
    private float speed;
    private int damage;
    
    private void Update()
    {
        if(rb is null){return;}
        rb.linearVelocity = dir * speed;
    }

    public void SetupProjectile(Vector2 direction, float projectileSpeed, int projectileDamage)
    {
        rb = GetComponent<Rigidbody2D>();
        
        dir = direction;
        dir.Normalize();
        speed = projectileSpeed;
        damage = projectileDamage;
    }
}
