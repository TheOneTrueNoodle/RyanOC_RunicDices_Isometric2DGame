using System;
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
    [SerializeField] private bool useRandomPatrolPoints;
    public float orbitDistance = 2;
    
    [HideInInspector] public GameObject aggroTarget;
    
    // State Machine Variables
    private State currentState;
    public IdleState idleState = new IdleState();
    public ChaseState chaseState = new ChaseState();
    public OrbitState orbitState = new OrbitState();
    public AttackState attackState = new AttackState();

    private void Start()
    {
        currentState = idleState;
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
        
        currentState = newState;
        if (newState == null)
        {
            currentState = idleState;
            Debug.LogError("New State was equal to Null");
        }
        currentState.EnterState(this);
    }
}
