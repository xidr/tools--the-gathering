using UnityEngine;

namespace Tools.Visitor.V1.AllInOne
{
    public class HealthComponent : MonoBehaviour, IVisitable
    {
        public int health = 100;

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            Debug.Log("HealthComponent.Accept");
        }

    }
}