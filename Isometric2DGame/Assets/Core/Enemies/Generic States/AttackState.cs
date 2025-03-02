using UnityEngine;

public class AttackState : State
{
    private float windUp = 1;
    private float coolDown = 1;
    private Vector2 dir;

    private float attackState; // This handles the current state of the attack. 0 = Wind Up, 1 = Trigger Attack, 2 = Cool Down
    
    public override void EnterState(Enemy enemy)
    {
        // Stop momentum
        enemy.moveCharacter.Move(enemy.rb, Vector2.zero , 0);
        
        // Stop moving animation TODO Create a wind up animation
        enemy.anim.SetBool("Moving", false);
        Debug.Log(enemy.anim.GetBool("Moving"));
        
        // Get the attack direction
        dir = enemy.aggroTarget.transform.position - enemy.transform.position;
        
        // Set the wind uptime 
        windUp = enemy.attackWindUp;
        enemy.windUpAnimator.Play("WindUp");

        // Set the cool downtime
        coolDown = enemy.attackCoolDown;
        
        // Set attack state
        attackState = 0;
    }

    public override void UpdateState(Enemy enemy)
    {
        switch (attackState)
        {
            case 0: // Attack Wind Up
                windUp -= Time.deltaTime;
                if (windUp <= 0) {attackState++;}
                break;
            case 1: // Trigger Attack
                enemy.attackCharacter.TriggerAttack(dir, enemy.damage);
                attackState++;
                break;
            case 2: // Cooldown After Attack
                coolDown--;
                if(coolDown <= 0){ enemy.SwitchState(enemy.chaseState);}
                break;
        }
    }

    public override void ExitState(Enemy enemy)
    {
        // Set the wind uptime 
        windUp = enemy.attackWindUp;

        // Set the cool downtime
        coolDown = enemy.attackCoolDown;
        
        // Reset attack state
        attackState = 0;
    }
}
