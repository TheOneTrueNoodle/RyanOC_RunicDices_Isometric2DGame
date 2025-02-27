using UnityEngine;

public class ChaseState : State
{
    public override void EnterState(Enemy enemy)
    {
        //Nothing happens when entering the Chase State
    }

    public override void UpdateState(Enemy enemy)
    {
        Vector2 direction = enemy.aggroTarget.transform.position - enemy.transform.position;


        if (direction.magnitude <= enemy.orbitDistance)
        {
            enemy.SwitchState(enemy.orbitState);
        }
        
        direction.Normalize();
        
        enemy.moveCharacter.Move(enemy.rb, direction, enemy.speed);
    }

    public override void ExitState(Enemy enemy)
    {
        //Nothing happens when exiting the Chase State
    }
}
