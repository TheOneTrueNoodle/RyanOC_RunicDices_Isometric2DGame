using UnityEngine;

public class IdleState : State
{
    public override void EnterState(Enemy enemy)
    {
        // Reset Characters velocity and movement
        enemy.moveCharacter.Move(enemy.rb, Vector2.zero, 0);
    }

    public override void UpdateState(Enemy enemy)
    {
        //Nothing happens during update for the Idle State
    }

    public override void ExitState(Enemy enemy)
    {
        //Nothing happens when exiting the Idle State
    }
}
