using UnityEngine;

public class EnemyWithProjectile : Enemy
{
    [Header("Projectile Variables")]
    [SerializeField] private float minProjectileDelay;
    [SerializeField] private float maxProjectileDelay;
    
    public float projectileSpeed;
    public int projectileDamage;
    
    public Projectile projectilePrefab;
    
    // Hidden Variables
    private float currentProjectileDelay;
    
    // State for shooting projectile
    public ShootProjectileState ShootProjectileState = new ShootProjectileState();

    public override void Start()
    {
        // Call base enemy class start
        base.Start();
        
        currentProjectileDelay = Random.Range(minProjectileDelay, maxProjectileDelay);
    }

    public override void Update()
    {
        // Call base enemy class update
        base.Update();

        if (aggroTarget is null) {return;}
        
        currentProjectileDelay -= Time.deltaTime;

        if (currentProjectileDelay <= 0)
        {
            // Switch to shooting projectile state
            SwitchState(ShootProjectileState);
            
            // Reset variable
            currentProjectileDelay = Random.Range(minProjectileDelay, maxProjectileDelay);
        }
    }
}
