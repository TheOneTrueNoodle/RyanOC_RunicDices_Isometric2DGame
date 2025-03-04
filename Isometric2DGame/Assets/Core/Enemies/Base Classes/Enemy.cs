using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Movement Variables")] [SerializeField]
    public float speed;
    
    [Header("Component References")]
    public Rigidbody2D rb;
    public MoveCharacter moveCharacter;
    public Animator anim;
    public NavMeshAgent navMeshAgent;
    
    // Variables for movement that are not in the inspector
    [HideInInspector] public Vector2 moveDirection;

    // Variables to handle Enemy AI
    
    [Header("Patrol State")] 
    // Patrol Variables
    public List<GameObject> patrolPoints;
    public float patrolSpeed;
    public float arrivalDistance;
    public float moveDelay;
    [Space(1)]
    // Random Patrol Variables
    public bool useRandomPatrolPoints; 
    public float maxRandomPatrolPointDistance = 2f;
    
    [Header("Orbit State")] 
    // Orbit Variables
    public float orbitDistance = 2f;
    public float maxTimeToAttack = 2f;

    [Header("Attack State")]
    // Attack Variables
    public AttackCharacter attackCharacter;
    public float attackWindUp = 0.5f;
    public float attackCoolDown = 0.5f;
    public int damage;
    public Animator windUpAnimator;
    
    [HideInInspector] public GameObject aggroTarget;
    
    // State Machine Variables
    public Animator stateAnim;
    
    private State currentState;
    public IdleState idleState = new IdleState();
    public PatrolState patrolState = new PatrolState();
    public ChaseState chaseState = new ChaseState();
    public OrbitState orbitState = new OrbitState();
    public AttackState attackState = new AttackState();

    public virtual void Start()
    {
        // Set nav mesh component
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        
        // Set starting State
        currentState = patrolPoints.Count > 0 ? patrolState : idleState;
        currentState.EnterState(this);
    }

    public virtual void Update()
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
