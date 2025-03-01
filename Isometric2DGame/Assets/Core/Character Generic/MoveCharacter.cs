using System;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    // A bool to pause movement when necessary
    [HideInInspector] public bool canMove = true;
    
    // Variables to manage what kind of terrain a Character is walking on
    private float modifier = 1;
    
    // A function that moves a rigidbody
    public void Move(Rigidbody2D rb, Vector2 dir, float speed)
    {
        if (!canMove) { return; }
        
        dir = dir.normalized;
        rb.linearVelocity = dir * (speed * modifier);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider has entered");

        if (other.TryGetComponent<MoveModifier>(out MoveModifier moveModifier))
        {
            switch (moveModifier.groundType)
            {
                case GroundType.mud:
                    modifier = 0.5f;
                    break;
                case GroundType.ice:
                    break;
                case GroundType.none:
                    modifier = 1;
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        modifier = 1;
    }
}
