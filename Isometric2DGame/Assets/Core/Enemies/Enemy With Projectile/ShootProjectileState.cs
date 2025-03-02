using Unity.VisualScripting;
using UnityEngine;

public class ShootProjectileState : State
{
    public override void EnterState(Enemy enemy)
    {
        // Check if the enemy is of the correct class
        if (enemy.TryGetComponent<EnemyWithProjectile>(out EnemyWithProjectile enemyP))
        {
            // Get direction to target
            Vector2 dir = enemy.aggroTarget.transform.position - enemy.transform.position;
        
            // Instantiate projectile
            Projectile newProjectile = Object.Instantiate(enemyP.projectilePrefab);
            
            // Call setup function for new projectile
            newProjectile.SetupProjectile(dir, enemyP.projectileSpeed, enemyP.projectileDamage);
            
            enemy.SwitchState(enemy.chaseState);
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
