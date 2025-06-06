using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.Strategy.git_amend {
    public class HeadsUpDisplay : MonoBehaviour
    {
        // [Ser]
        [SerializeField] Button[] _buttons;
    
        public delegate void ButtonPressedEvent(int index);
    
        public static event ButtonPressedEvent OnButtonPressed;

        void Awake() {
            for (int i = 0; i < _buttons.Length; i++) {
                int index = i;
                _buttons[i].onClick.AddListener(() => HandleButtonPress(index));
            }
        }

        void HandleButtonPress(int index) => OnButtonPressed?.Invoke(index);
    }
}