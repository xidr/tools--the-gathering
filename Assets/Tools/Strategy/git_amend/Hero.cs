using UnityEngine;

namespace Tools.Strategy.git_amend {
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