using UnityEngine;
using UnityEngine.UI;

namespace Tools.Strategy.git_amend {
    public class HeadsUpDisplay : MonoBehaviour
    {
        // [Ser]
        [SerializeField] Button[] _buttons;
    
        public delegate void ButtonPressedEvent(int index);
    
        public static event ButtonPressedEvent OnButtonPressed;
    
    

    }
}