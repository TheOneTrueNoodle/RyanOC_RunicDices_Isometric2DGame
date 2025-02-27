using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerHandler : MonoBehaviour
{
    [FormerlySerializedAs("_speed")] [Header("Movement Variables")] [SerializeField]
    private float speed;

    [FormerlySerializedAs("_rb")] [Header("Component References")]
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private MoveCharacter moveCharacter;
    
    // Variables for movement that are not in the inspector
    [HideInInspector] public Vector2 moveDirection;
    private PlayerInputMap _input;

    private void FixedUpdate()
    {
        moveCharacter.Move(rb, moveDirection, speed);
    }

    private void SetMovementInput(InputAction.CallbackContext ctx)
    {
        // Get the Movement Input
        Vector2 moveInput = ctx.ReadValue<Vector2>();

        // Get the Radian for 45 degrees. This is to apply a -45 degree angle to the movement input, so pressing W or Up moves the Character to the North East
        float delta = -45.0f * Mathf.Deg2Rad;
        
        // Calculate new x and y
        float x = moveInput.x * Mathf.Cos(delta) - moveInput.y * Mathf.Sin(delta);
        float y = moveInput.x * Mathf.Sin(delta) + moveInput.y * Mathf.Cos(delta);
        
        // Assign new x and y as moveDirection
        moveDirection = new Vector2(x, y);
    }
    
    private void OnEnable()
    {
        // Create a new instance of the Player Input Map.
        _input = new PlayerInputMap();
        _input.Gameplay.Enable();
        
        // Subscribes the SetMovementInput() function to the Movement Player Input.
        _input.Gameplay.Movement.performed += SetMovementInput;
        _input.Gameplay.Movement.canceled += SetMovementInput;
    }

    private void OnDisable()
    {
        // Unsubscribes the SetMovementInput() function to the Movement Player Input.
        _input.Gameplay.Movement.performed -= SetMovementInput;
        _input.Gameplay.Movement.canceled -= SetMovementInput;
    }
}
