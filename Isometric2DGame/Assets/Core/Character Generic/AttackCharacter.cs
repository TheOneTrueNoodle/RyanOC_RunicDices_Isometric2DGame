using Unity.VisualScripting;
using UnityEngine;

public class AttackCharacter : MonoBehaviour
{
    // Inspector variables
    [SerializeField] private GameObject parentObj;
    [SerializeField] private Animator attackAnim;
    [SerializeField] private HitBox hitBox;
    public HurtBox hurtBox;
    
    // Bool to prevent attacking while attacking
    [HideInInspector] public bool isAttacking = false;
    
    // Attack needs 2 inputs. Direction to attack and damage of attack
    public void TriggerAttack(Vector2 direction, int damage)
    {
        // Toggle the isAttacking bool so we do not have duplicate attacks
        isAttacking = true;
        Debug.Log("Starting Attack");
        
        // Set the rotation of the attack
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        parentObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        //  Set up the Hit Box of the attack
        hitBox.Setup(hurtBox, damage);
        
        // Start Attack Animation
        attackAnim.Play("Attack");
    }

    public void CompleteAttack()
    {
        isAttacking = false;;
        Debug.Log("Completed Attack");
        
        // Disable Hit Box
        hitBox.DisableHitBox();
    }
}
