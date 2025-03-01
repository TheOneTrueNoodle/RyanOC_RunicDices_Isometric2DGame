using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    private NavMeshPath path;

    private float _reCalculatePathTimer = 0;
    private const float reCalcTime = 0.2f;

    private int pathIndex;
    
    public override void EnterState(Enemy enemy)
    {
        // Debug entered Chase State
        Debug.Log(enemy.gameObject.name + " has entered the Chase State");
        
        // Reset old path
        path = new NavMeshPath();
        pathIndex = 1;
        
        // Calculate a path to the target
        NavMesh.CalculatePath(enemy.transform.position, enemy.aggroTarget.transform.position, NavMesh.AllAreas, path);
        
        // Set re calculate timer to prevent recalculating the path every single frame
        _reCalculatePathTimer = reCalcTime;
    }

    public override void UpdateState(Enemy enemy)
    {
        // Check if the path has any corners, if not generate path
        if (path.corners.Length <= 0)
        {
            // Reset old path
            path = new NavMeshPath();
            pathIndex = 1;
            
            // Calculate a path to the target
            NavMesh.CalculatePath(enemy.transform.position, enemy.aggroTarget.transform.position, NavMesh.AllAreas, path);
        }
        
        // Update recalculate path timer
        _reCalculatePathTimer -= Time.deltaTime;
        
        // If timer runs out, recalculate the path to target
        if (_reCalculatePathTimer <= 0)
        {
            // Reset old path
            path = new NavMeshPath();
            pathIndex = 1;
            
            // Calculate a path to the target
            NavMesh.CalculatePath(enemy.transform.position, enemy.aggroTarget.transform.position, NavMesh.AllAreas, path);
            _reCalculatePathTimer = reCalcTime;
        }
        
        // Find the correct node in the path to go to
        if ((enemy.transform.position - path.corners[pathIndex]).magnitude < 0.1f){pathIndex++;}
        
        // Find the direction to the enemies target
        Vector2 direction = path.corners[pathIndex] - enemy.transform.position;
        
        // Change to orbit state when the enemy is close enough
        float distanceToTarget = (enemy.aggroTarget.transform.position - enemy.transform.position).magnitude;
        if (distanceToTarget <= enemy.orbitDistance)
        {
            enemy.SwitchState(enemy.orbitState);
        }
        
        // Normalize direction
        direction.Normalize();
        Debug.Log(direction);
        
        // Set Animator Variables
        enemy.anim.SetBool("Moving", direction != Vector2.zero);
        if (direction != Vector2.zero)
        {
            enemy.anim.SetFloat("Vertical", direction.y);
            enemy.anim.SetFloat("Horizontal", direction.x);
        }
        
        // Move the enemy towards the target
        enemy.moveCharacter.Move(enemy.rb, direction, enemy.speed);
    }

    public override void ExitState(Enemy enemy)
    {
        //Nothing happens when exiting the Chase State
    }
}
