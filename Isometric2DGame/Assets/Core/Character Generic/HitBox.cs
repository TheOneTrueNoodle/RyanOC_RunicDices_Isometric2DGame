using UnityEngine;

public class HitBox : MonoBehaviour
{
    // Inspector variables
    [Header("Components")]
    [SerializeField] private Collider2D col;

    [Header("Destroy on hit")]
    public bool destroyOnHit = false;
    public GameObject destroyOnHitObj;
    
    [Header("Properties")]
    public bool useHitBoxForKnockback = false;
    public HurtBox originHurtBox;
    public int damage = 1;
    public float knockbackStrength;

    public void Setup(HurtBox origin, int dmg)
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
