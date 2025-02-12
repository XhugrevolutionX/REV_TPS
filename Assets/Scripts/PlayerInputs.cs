using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private Vector2 _move;
    private bool _isRunning;
    private bool _isJumping;
    
    public Vector2 Move => _move;
    public bool IsRunning => _isRunning;
    
    public void OnMove(InputAction.CallbackContext context) => _move = context.ReadValue<Vector2>();
    
    public void OnRun(InputAction.CallbackContext context) => _isRunning = context.ReadValueAsButton();
}
