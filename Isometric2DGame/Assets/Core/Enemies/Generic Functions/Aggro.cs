using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    [SerializeField] private Enemy thisEnemy;
    [SerializeField] private Collider2D aggroCollider;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        thisEnemy.aggroTarget = other.gameObject;
        
        // Play Alert Animation
        thisEnemy.stateAnim.Play("Alert");
        
        thisEnemy.SwitchState(thisEnemy.chaseState);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        thisEnemy.aggroTarget = null;
        State newState = thisEnemy.patrolPoints.Count > 0 ? thisEnemy.patrolState : thisEnemy.idleState;
        
        // Play lost animation
        thisEnemy.stateAnim.Play("Lost");
        thisEnemy.SwitchState(newState);
    }
}
