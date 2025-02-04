using UnityEngine;

namespace Tools.Visitor.V1.AllInOne
{
    public class ManaComponent : MonoBehaviour, IVisitable
    {
        private int _mana = 100;

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            Debug.Log("ManaComponent.Accept");
        }

        public void AddMana(int amount)
        {
            _mana += amount;
        }
    }
}