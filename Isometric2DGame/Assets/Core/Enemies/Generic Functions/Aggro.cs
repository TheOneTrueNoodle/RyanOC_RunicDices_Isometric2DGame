using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    [SerializeField] private Enemy thisEnemy;
    [SerializeField] private Collider2D aggroCollider;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thisEnemy.aggroTarget = other.gameObject;
            thisEnemy.SwitchState(thisEnemy.chaseState);
        }
    }
}
