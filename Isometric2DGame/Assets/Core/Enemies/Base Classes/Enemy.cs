using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Movement Variables")] [SerializeField]
    public float speed;
    
    [Header("Component References")]
    public Rigidbody2D rb;
    public MoveCharacter moveCharacter;
    public Animator anim;
    
    // Variables for movement that are not in the inspector
    [HideInInspector] public Vector2 moveDirection;

    // Variables to handle Enemy AI
    [Header("AI Variables")] 
    // Patrol Variables
    [SerializeField] private bool useRandomPatrolPoints;
    public List<GameObject> patrolPoints;
    public float patrolSpeed;
    public float arrivalDistance;
    public float moveDelay;
    
    // Orbit Variables
    public float orbitDistance = 2;
    
    [HideInInspector] public GameObject aggroTarget;
    
    // State Machine Variables
    private State currentState;
    public IdleState idleState = new IdleState();
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public OrbitState orbitState = new OrbitState();
    public AttackState attackState = new AttackState();

    private void Start()
    {
        currentState = patrolPoints.Count > 0 ? patrolState : idleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        // Call the Update State function every frame.
        currentState.UpdateState(this);
    }

    public void SwitchState(State newState)
    {
        // Call the Exit State function of a state.
        currentState.ExitState(this);
        
        // Set the current state to the new state
        currentState = newState;
        
        // Check if there is a new state, if not, set state to idle.
        if (newState == null)
        {
            currentState = idleState;
            Debug.LogError("New State was equal to Null");
        }
        currentState.EnterState(this);
    }
}
