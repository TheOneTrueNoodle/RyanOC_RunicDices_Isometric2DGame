using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    // General patrol variables
    private Vector3 targetPosition;
    private int _currentIndex = -1;
    private float _moveDelay = 0;
    private bool _wait = false;
    
    // Random patrol variables
    private float randomDistance = 0;
    private float expectedTravelTime = 1;
    
    // Pathfinding variables
    private NavMeshPath path;

    private int pathIndex;
    
    public override void EnterState(Enemy enemy)
    {
        // Debug entered Patrol State
        Debug.Log(enemy.gameObject.name + " has entered the Patrol State");
        
        // Quick check to make sure there are patrol points
        if (enemy.patrolPoints.Count <= 0){enemy.SwitchState(enemy.idleState);}
        
        // Reset important variable for patrolling
        _wait = false;
        float shortestDistance = 99999;
        _currentIndex = -1;
        
        // Check if we want to use a random patrol point
        if (enemy.useRandomPatrolPoints)
        {
            GetRandomPatrolPoint(enemy);
            return;
        }
        
        // Loop through each patrol point and find which one is closest to start at.
        for (int i = 0; i < enemy.patrolPoints.Count; i++)
        {
            float distance = (enemy.patrolPoints[i].transform.position - enemy.transform.position).magnitude;
            if (shortestDistance > distance)
            {
                shortestDistance = distance;
                _currentIndex = i;
            }
        }
        targetPosition = enemy.patrolPoints[_currentIndex].transform.position;
        
        // Reset old path
        path = new NavMeshPath();
        pathIndex = 1;
        
        // Calculate a path to the target position
        NavMesh.CalculatePath(enemy.transform.position, targetPosition, NavMesh.AllAreas, path);
    }

    public override void UpdateState(Enemy enemy)
    {
        if(_wait){WaitToMove(enemy);}
        else{MoveTowardsPatrolPoint(enemy);}
    }

    public override void ExitState(Enemy enemy)
    {
        // Nothing happens when exiting the Patrol State.
    }

    private void WaitToMove(Enemy enemy)
    {
        // Count down delay
        _moveDelay -= Time.deltaTime;
        if (_moveDelay > 0) { return; }
        
        // When the delay is over change patrol point
        // First check if we want to get a random patrol point
        if(enemy.useRandomPatrolPoints){GetRandomPatrolPoint(enemy);}
        else{GetNextPatrolPoint(enemy);}
    }

    private void MoveTowardsPatrolPoint(Enemy enemy)
    {
        // Check if the path has any corners, if not generate path
        if (path.corners.Length <= 0)
        {
            // Reset old path
            path = new NavMeshPath();
            pathIndex = 1;
            
            // Calculate a path to the target
            NavMesh.CalculatePath(enemy.transform.position, targetPosition, NavMesh.AllAreas, path);
        }
        
        // Check if close enough to patrol point to wait
        float distance = (targetPosition - enemy.transform.position).magnitude;
        if (distance < enemy.arrivalDistance)
        {
            _wait = true;
            _moveDelay = enemy.moveDelay;
            
            // Stop momentum
            enemy.moveCharacter.Move(enemy.rb, Vector2.zero, 0);
            
            return;
        }
        
        // Check if we are using a random patrol point. If true, reduce expected travel time to ensure we dont get stuck.
        if (enemy.useRandomPatrolPoints) { expectedTravelTime -= Time.deltaTime; }
        
        // Move towards the patrol point
        
        // Check if expected travel time has run out OR if the enemy has reached the end of the path.
        if (expectedTravelTime <= 0 || pathIndex >= path.corners.Length)
        {
            _wait = true;
            _moveDelay = enemy.moveDelay;
            
            // Stop momentum
            enemy.moveCharacter.Move(enemy.rb, Vector2.zero, 0);
            
            return;
        }
        
        // Find the correct node in the path to go to
        if ((enemy.transform.position - path.corners[pathIndex]).magnitude < 0.1f)
        {
            pathIndex++;
        }
        
        // Find the direction to the next node
        Vector2 direction = path.corners[pathIndex] - enemy.transform.position;
        // Normalize direction
        direction.Normalize();
        
        // Move the enemy towards the target node
        enemy.moveCharacter.Move(enemy.rb, direction, enemy.speed);
    }

    private void GetNextPatrolPoint(Enemy enemy)
    {
        // Reset wait bool and Increase index
        _wait = false;
        _currentIndex++;
        
        // Check if index is bigger than patrol points. If true, reset index to 0.
        if (_currentIndex >= enemy.patrolPoints.Count)
        {
            _currentIndex = 0;
        }
        
        // Set new patrol point.
        targetPosition = enemy.patrolPoints[_currentIndex].transform.position;
        
        // Reset old path
        path = new NavMeshPath();
        pathIndex = 1;
            
        // Calculate a path to the target
        NavMesh.CalculatePath(enemy.transform.position, targetPosition, NavMesh.AllAreas, path);
    }
    
    private void GetRandomPatrolPoint(Enemy enemy)
    {
        // Reset wait bool
        _wait = false;
        
        // Pick a random direction
        float randomX = Random.Range(-1, 2);
        float randomY = Random.Range(-1, 2);
        Vector3 randomDirection = new Vector2(randomX, randomY);
        randomDirection.Normalize();
        
        // Choose a randomised time to travel for.
        randomDistance = Random.Range(0.1f, enemy.maxRandomPatrolPointDistance);
        
        // Calculate expected travel time
        expectedTravelTime = randomDistance / enemy.patrolSpeed;
        
        // Set new patrol point.
        targetPosition = enemy.transform.position + (randomDirection * randomDistance);
        
        // Reset old path
        path = new NavMeshPath();
        pathIndex = 1;
            
        // Calculate a path to the target
        NavMesh.CalculatePath(enemy.transform.position, targetPosition, NavMesh.AllAreas, path);
    }
}
