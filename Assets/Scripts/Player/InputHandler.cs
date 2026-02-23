using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // Private variables
    private Vector2 moveInput; // Left and right movement


    // Public properties
    public Vector2 MoveInput
    {
        get { return moveInput; }
    }

    // Public methods
    public void OnMove(InputAction.CallbackContext context)
    {
        // Save movmement input into moveInput field
        moveInput = context.ReadValue<Vector2>();
    }

}
