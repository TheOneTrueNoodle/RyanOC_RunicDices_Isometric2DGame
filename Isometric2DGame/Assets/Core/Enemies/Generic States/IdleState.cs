using UnityEngine;

public class IdleState : State
{
    public override void EnterState(Enemy enemy)
    {
        //Nothing happens when entering the Idle State
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
