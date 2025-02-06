using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.InputSystem.ReadDirectly
{
    public class InputReader
    {

        private Vector2 _moveInput = Vector2.zero;

        private void Start()
        {
            
        }

        private void Update()
        {
            Gamepad gamepad = Gamepad.current;
            if (gamepad != null) return;
            
            _moveInput = gamepad.leftStick.ReadValue();

            if (gamepad.crossButton.wasPressedThisFrame)
            {
                Debug.Log("Gamepad cross button was pressed");
            }
        }
        
    }
}