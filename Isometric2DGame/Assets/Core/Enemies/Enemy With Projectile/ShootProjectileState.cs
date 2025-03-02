using Unity.VisualScripting;
using UnityEngine;

public class ShootProjectileState : State
{
    public override void EnterState(Enemy enemy)
    {
        // Check if the enemy is of the correct class
        if (enemy.TryGetComponent(out EnemyWithProjectile enemyP))
        {
            // Get direction to target
            Vector2 dir = enemyP.aggroTarget.transform.position - enemyP.transform.position;
            dir.Normalize();
        
            // Instantiate projectile
            Projectile newProjectile = Object.Instantiate(enemyP.projectilePrefab).GetComponent<Projectile>();
            newProjectile.transform.position = enemyP.transform.position;
            
            // Call setup function for new projectile
            Debug.Log(enemyP);
            Debug.Log(newProjectile);
            
            newProjectile.SetupProjectile(dir, enemyP.projectileSpeed, enemyP.projectileDamage, enemyP.attackCharacter.hurtBox);
            
            // Reset Enemy 
            enemyP.currentProjectileDelay = Random.Range(enemyP.minProjectileDelay, enemyP.maxProjectileDelay);
            
            enemyP.SwitchState(enemyP.chaseState);
        }
        else
        {
            // If enemy is the wrong class ,return to chase state
            enemy.SwitchState(enemy.chaseState);
            Debug.LogError("Enemy is of the wrong class for the Shoot Projectile State");
        }
    }

    public override void UpdateState(Enemy enemy)
    {
    }

    public override void ExitState(Enemy enemy)
    {
    }
}
