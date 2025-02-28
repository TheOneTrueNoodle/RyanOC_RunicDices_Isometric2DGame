using UnityEngine;

public class PatrolState : State
{
    private int _currentIndex = -1;
    private float _moveDelay = 0;
    private bool _wait = false;
    
    public override void EnterState(Enemy enemy)
    {
        // Quick check to make sure there are patrol points
        if (enemy.patrolPoints.Count <= 0){enemy.SwitchState(enemy.idleState);}
        
        // Reset important variable for patrolling
        _wait = false;
        float shortestDistance = 99999;
        _currentIndex = -1;
        
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
    }

    public override void UpdateState(Enemy enemy)
    {
        if(_wait){waitToMove(enemy);}
        else{moveTowardsPatrolPoint(enemy);}
    }

    public override void ExitState(Enemy enemy)
    {
        // Nothing happens when exiting the Patrol State.
    }

    private void waitToMove(Enemy enemy)
    {
        Debug.Log("Waiting to move");
        
        // Count down delay
        _moveDelay -= Time.deltaTime;
        if (_moveDelay > 0) { return; }
        
        // When the delay is over change patrol point
        _wait = false;
        _currentIndex++;
        if (_currentIndex >= enemy.patrolPoints.Count)
        {
            _currentIndex = 0;
        }
    }

    private void moveTowardsPatrolPoint(Enemy enemy)
    {
        // Check if close enough to patrol point to wait
        Debug.Log(enemy.patrolPoints[_currentIndex].name);
        float distance = (enemy.patrolPoints[_currentIndex].transform.position - enemy.transform.position).magnitude;
        if (distance < enemy.arrivalDistance)
        {
            _wait = true;
            _moveDelay = enemy.moveDelay;
            
            // Stop momentum
            enemy.moveCharacter.Move(enemy.rb, Vector2.zero, 0);
            
            return;
        }
        
        // Move towards next patrol point
        Vector2 direction = enemy.patrolPoints[_currentIndex].transform.position - enemy.transform.position;
        direction.Normalize();
        enemy.moveCharacter.Move(enemy.rb, direction, enemy.patrolSpeed);
    }
}
