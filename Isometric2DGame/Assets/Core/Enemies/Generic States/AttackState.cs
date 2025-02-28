using UnityEngine;

public class AttackState : State
{
    public override void EnterState(Enemy enemy)
    {
        Debug.Log("Enemy has attacked!");
        enemy.SwitchState(enemy.chaseState);
    }

    public override void UpdateState(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }
}
