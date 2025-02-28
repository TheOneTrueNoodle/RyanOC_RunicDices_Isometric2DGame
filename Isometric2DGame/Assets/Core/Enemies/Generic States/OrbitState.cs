using UnityEngine;

public class OrbitState : State
{
    private float orbitDir = 1;
    private float angle = 0;

    private float timeToAttack = 0;
    
    public override void EnterState(Enemy enemy)
    {
        // Randomly pick a direction on whether to orbit clockwise or anti-clockwise around target.
        orbitDir = Random.Range(0f, 1f);
        orbitDir = orbitDir == 0 ? -1 : 1;
        
        // Calculate the current angle towards the target.
        angle = Vector2.Angle(enemy.transform.position, enemy.aggroTarget.transform.position);
        
        // Set a random time to attack.
        timeToAttack = Random.Range(0.1f, enemy.maxTimeToAttack);
    }

    public override void UpdateState(Enemy enemy)
    {
        // Check if the enemy is too far away to orbit. If too far, go back to Chase State.
        if ((enemy.aggroTarget.transform.position - enemy.transform.position).magnitude >= enemy.orbitDistance * 2)
        {
            enemy.SwitchState(enemy.chaseState);
        }
        
        // Count down the time to attack
        timeToAttack -= Time.deltaTime;
        
        // Check if it is time to attack
        if(timeToAttack <= 0){enemy.SwitchState(enemy.attackState);}
        
        // Increment the angle during orbit based on the delta time.
        angle += Time.deltaTime * orbitDir * (enemy.speed / 3);
        
        // Calculate the new Vector 2 position in orbit while maintaining orbit distance.
        float x = enemy.aggroTarget.transform.position.x + Mathf.Cos(angle) * enemy.orbitDistance;
        float y = enemy.aggroTarget.transform.position.y + Mathf.Sin(angle) * enemy.orbitDistance;
        Vector3 target = new Vector2(x, y);
        
        // Get the direction towards the new orbit position
        Vector2 direction = target - enemy.transform.position;
        direction.Normalize();
        
        // Move the enemy towards the new position in orbit
        enemy.moveCharacter.Move(enemy.rb, direction , (enemy.speed / 3));
    }

    public override void ExitState(Enemy enemy)
    {
        //Nothing happens when exiting the Orbit State
    }
}
