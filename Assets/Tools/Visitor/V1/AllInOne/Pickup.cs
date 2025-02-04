using UnityEngine;

namespace Tools.Visitor.V1.AllInOne
{
    public class Pickup : MonoBehaviour
    {
        public PowerUp powerUp;

        void OnTriggerEnter(Collider other)
        {
            var visitable = other.GetComponent<IVisitable>();
            if (visitable != null)
            {
                visitable.Accept(powerUp);
                Destroy(gameObject);
            }
        }
    }
}