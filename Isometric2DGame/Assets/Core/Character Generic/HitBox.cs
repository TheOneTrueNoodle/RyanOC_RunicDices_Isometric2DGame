using UnityEngine;

public class HitBox : MonoBehaviour
{
    // Inspector variables
    [SerializeField] private Collider2D col;
    
    public HurtBox originHurtBox;
    public float damage;
    public float knockbackStrength;

    public void Setup(HurtBox origin, float dmg)
    {
        col.enabled = true;
        originHurtBox = origin;
        damage = dmg;
    }

    public void DisableHitBox()
    {
        col.enabled = false;
    }
}
