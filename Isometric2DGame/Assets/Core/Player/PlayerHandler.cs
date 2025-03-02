using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerHandler : MonoBehaviour
{
    [Header("Movement Variables")] [SerializeField]
    private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MoveCharacter moveCharacter;
    [SerializeField] private Animator anim;

    [Header("Attack Variables")]
    [SerializeField] private AttackCharacter attackCharacter;
    [SerializeField] private float damage;
    
    // Variables for movement that are not in the inspector
    [HideInInspector] public Vector2 moveDirection;
    private PlayerInputMap _input;
    private Vector2 currentFaceDirection = new Vector2(1, 1);

    private void FixedUpdate()
    {
        moveCharacter.Move(rb, moveDirection, speed);
    }

    private void SetMovementInput(InputAction.CallbackContext ctx)
    {
        // Get the Movement Input
        Vector2 moveInput = ctx.ReadValue<Vector2>();
        

        // Get the exact Radians to align the movement input with the world tileset so pressing W or Up moves the Character to the North East in line with the tiles.
        float deltaX = -63.0f * Mathf.Deg2Rad;
        float deltaY = -27.0f * Mathf.Deg2Rad;
        
        // Calculate new x and y
        float x = moveInput.x * Mathf.Cos(deltaY) - moveInput.y * Mathf.Sin(deltaX);
        float y = moveInput.x * Mathf.Sin(deltaY) + moveInput.y * Mathf.Cos(deltaX);
        
        // Assign new x and y as moveDirection
        moveDirection = new Vector2(x, y);
        
        // Set Animator Variables
        anim.SetBool("Moving", moveInput != Vector2.zero);
        if (moveInput != Vector2.zero)
        {
            anim.SetFloat("Vertical", moveInput.y);
            anim.SetFloat("Horizontal", moveInput.x);
            
            // Lets set the current face direction
            float faceX = x > 0 ? 1 : -1;
            float faceY = y > 0 ? 1 : -1;
            currentFaceDirection = new Vector2(faceX, faceY);
        }
    }

    private void TriggerAttack(InputAction.CallbackContext ctx)
    {
        if(attackCharacter.isAttacking){return;}
        
        // Call attack code
        attackCharacter.TriggerAttack(currentFaceDirection, damage);
    }
    
    private void OnEnable()
    {
        // Create a new instance of the Player Input Map.
        _input = new PlayerInputMap();
        _input.Gameplay.Enable();
        
        // Subscribes the SetMovementInput() function to the Movement Player Input.
        _input.Gameplay.Movement.performed += SetMovementInput;
        _input.Gameplay.Movement.canceled += SetMovementInput;
        
        // Subscribes the StartAttack() function to the Attack Player Input
        _input.Gameplay.Attack.performed += TriggerAttack;
    }

    private void OnDisable()
    {
        // Unsubscribes the SetMovementInput() function to the Movement Player Input.
        _input.Gameplay.Movement.performed -= SetMovementInput;
        _input.Gameplay.Movement.canceled -= SetMovementInput;
        
        // Unsubscribes the StartAttack() function to the Attack Player Input
        _input.Gameplay.Attack.performed -= TriggerAttack;
    }
}
