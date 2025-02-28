using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    private NavMeshPath path;

    private float _reCalculatePathTimer = 0;
    private const float reCalcTime = 0.2f;
    
    public override void EnterState(Enemy enemy)
    {
        // Calculate a path to the Player
        path = new NavMeshPath();
        enemy.navMeshAgent.CalculatePath(enemy.aggroTarget.transform.position, path);
        
        Debug.Log(path);
        Debug.Log(path.corners.Length);
        
        // Set re calculate timer to prevent recalculating the path every single frame
        _reCalculatePathTimer = reCalcTime;
    }

    public override void UpdateState(Enemy enemy)
    {
        // Check if the path has any corners
        if (path.corners.Length <= 0)
        {
            path = new NavMeshPath();
            enemy.navMeshAgent.CalculatePath(enemy.aggroTarget.transform.position, path);
            Debug.Log(path.corners.Length);
        }
        
        // Update recalculate path timer
        _reCalculatePathTimer -= Time.deltaTime;
        // If timer runs out, recalculate the path to target
        if (_reCalculatePathTimer <= 0)
        {
            path = new NavMeshPath();
            enemy.navMeshAgent.CalculatePath(enemy.aggroTarget.transform.position, path);
            _reCalculatePathTimer = reCalcTime;
        }
        
        // Find the direction to the enemies target
        Vector2 direction = path.corners[0] - enemy.transform.position;
        
        // Change to orbit state when the enemy is close enough
        if (direction.magnitude <= enemy.orbitDistance)
        {
            enemy.SwitchState(enemy.orbitState);
        }
        
        // Normalize direction
        direction.Normalize();
        
        // Move the enemy towards the target
        enemy.moveCharacter.Move(enemy.rb, direction, enemy.speed);
    }

    public override void ExitState(Enemy enemy)
    {
        //Nothing happens when exiting the Chase State
    }
}
