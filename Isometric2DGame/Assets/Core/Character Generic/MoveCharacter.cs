using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public void Move(Rigidbody2D rb, Vector2 dir, float speed)
    {
        rb.linearVelocity = dir * speed;
    }
}
