using UnityEngine;

public class OrbitState : State
{
    private float orbitDir = 1;
    private float angle = 0;
    
    public override void EnterState(Enemy enemy)
    {
        orbitDir = Random.Range(0f, 1f);
        orbitDir = orbitDir == 0 ? -1 : 1;
        
        angle = Vector2.Angle(enemy.transform.position, enemy.aggroTarget.transform.position);
    }

    public override void UpdateState(Enemy enemy)
    {
        
        if ((enemy.aggroTarget.transform.position - enemy.transform.position).magnitude >= enemy.orbitDistance * 2)
        {
            enemy.SwitchState(enemy.chaseState);
        }
        
        angle += Time.deltaTime * orbitDir * (enemy.speed / 3);
        
        float x = enemy.aggroTarget.transform.position.x + Mathf.Cos(angle) * enemy.orbitDistance;
        float y = enemy.aggroTarget.transform.position.y + Mathf.Sin(angle) * enemy.orbitDistance;
        
        Vector3 target = new Vector2(x, y);
        
        Vector2 direction = target - enemy.transform.position;
        direction.Normalize();
        
        enemy.moveCharacter.Move(enemy.rb, direction , (enemy.speed / 3));
    }

    public override void ExitState(Enemy enemy)
    {
    }
}
