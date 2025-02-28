using UnityEngine;

public class ChaseState : State
{
    public override void EnterState(Enemy enemy)
    {
        //Nothing happens when entering the Chase State
    }

    public override void UpdateState(Enemy enemy)
    {
        // Find the direction to the enemies target
        Vector2 direction = enemy.aggroTarget.transform.position - enemy.transform.position;
        
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
