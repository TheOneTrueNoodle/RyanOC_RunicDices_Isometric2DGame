using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Movement Variables")] [SerializeField]
    private float speed;
    
    [Header("Component References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MoveCharacter moveCharacter;
    [SerializeField] private Animator anim;
    
    // Variables for movement that are not in the inspector
    [HideInInspector] public Vector2 moveDirection;
    
    // State Machine Variables
    public State currentState;

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(State newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
