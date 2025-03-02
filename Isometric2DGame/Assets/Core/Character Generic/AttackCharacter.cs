using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class AttackCharacter : MonoBehaviour
{
    // Inspector variables
    [SerializeField] private GameObject parentObj;
    [SerializeField] private Animator attackAnim;
    [SerializeField] private HitBox hitBox;
    public HurtBox hurtBox;
    
    // Bool to prevent attacking while attacking
    [HideInInspector] public bool isAttacking = false;
    private MoveCharacter moveCharacter;
    
    // Ranged attack cooldown
    private bool isRangedAttacking = false;
    [SerializeField] private float rangedCooldown = 0.3f;
    private float currentRangedCooldown;

    private void Update()
    {
        if(!isRangedAttacking){return;}
        
        currentRangedCooldown -= Time.deltaTime;
        if (currentRangedCooldown <= 0)
        {
            CompleteAttack();
        }
    }

    // Attack needs 2 inputs. Direction to attack and damage of attack
    public void TriggerAttack(Vector2 direction, int damage, MoveCharacter move)
    {
        // Toggle the isAttacking bool so we do not have duplicate attacks
        isAttacking = true;
        
        // Stop movement while attacking
        moveCharacter = move;
        moveCharacter.canMove = false;
        
        
        // Set the rotation of the attack
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        parentObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        //  Set up the Hit Box of the attack
        hitBox.Setup(hurtBox, damage);
        
        // Start Attack Animation
        attackAnim.Play("Attack");
    }

    public void TriggerRangedAttack(Vector2 direction, Projectile projectilePrefab, int damage, float speed, MoveCharacter move)
    {
        // Toggle the isAttacking bool so we do not have duplicate attacks
        isAttacking = true;
        
        // Stop movement while attacking
        moveCharacter = move;
        moveCharacter.canMove = false;
        
        // Instantiate projectile
        Projectile newProjectile = Object.Instantiate(projectilePrefab).GetComponent<Projectile>();
        newProjectile.transform.position = parentObj.transform.position;
            
        // Call setup function for new projectile
        newProjectile.SetupProjectile(direction, speed, damage, hurtBox);
        
        // Set up ranged attack cooldown
        currentRangedCooldown = rangedCooldown;
        isRangedAttacking = true;
    }

    public void CompleteAttack()
    {
        isAttacking = false;
        isRangedAttacking = false;
        
        // Disable Hit Box
        hitBox.DisableHitBox();
        moveCharacter.canMove = true;
    }
}
