using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.InputSystem.PlayerInputComponent
{
    public class InputReader : MonoBehaviour
    {

        private Vector2 _moveInput = Vector2.zero;

        private PlayerInput _playerInput;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();

            _playerInput.onActionTriggered += HandleInputAction;
        }
        
        private void Update()
        {
            
        }

        private void HandleInputAction(InputAction.CallbackContext context)
        {
            switch (context.action.name)
            {
                case "Move":
                    _moveInput = context.ReadValue<Vector2>();
                    break;
                case "Jump":
                    if (context.performed)
                    {
                        Debug.Log("Jump");
                    }
                    break;
            }
        }

        public void Move(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        // Started, Performed, Canceled
        
        public void Jump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("Jump");
            }
            
            if (context.action.IsPressed())
            {
                Debug.Log("Jump");
            }
            
            if (context.action.WasPressedThisFrame())
            {
                Debug.Log("Jump");
            }
        }
        
    }
}