using UnityEngine;

namespace Patterns.Strategy.GA {
    public class Hero : MonoBehaviour {
        [SerializeField] SpellStrategy[] _spells;

        void OnEnable() {
            HeadsUpDisplay.OnButtonPressed += CastSpell;
        }

        void OnDisable() {
            HeadsUpDisplay.OnButtonPressed -= CastSpell;
        }

        void CastSpell(int index) {
            _spells[index].CastSpell(transform);
        }
    }
}