using System.Linq;
using UnityEngine;

namespace Tools.Extension_Methods
{
    public class UsageExamples : MonoBehaviour
    {

        [SerializeField]
        private GameObject _powerOrbPrefab;
        [SerializeField]
        private float _powerOrbHeight = 1f;
        public void Vector3Example()
        {
            var powerOrb = Instantiate(_powerOrbPrefab, transform.position.With(y: _powerOrbHeight).Add(x : .0f, y : 1f),
                Quaternion.identity);
        }

        [SerializeField]
        private Light _defaultLight;
        
        public void GameObjectExample()
        {
            gameObject.GetOrAdd<Light>().enabled = true;

            // Easy alternative value for the case when there isn't the component in question
            var color = gameObject.GetComponent<Renderer>().OrNull()?.material.color ?? new Color(1, 1, 1, 1);
            
            // Another option - to use some default component if there aren't any
            var light = gameObject.GetComponent<Light>().OrNull() ?? _defaultLight;
            
            gameObject.DestroyChildren();
        }

        public void TransformExample()
        {
            foreach (Transform child in transform.Children())
            {
                Debug.Log(child.name);
            }
            
            // Or simply, as `Transform` implements IEnumerable
            foreach (Transform child in transform)
            {
                Debug.Log(child.name);
            }
            
            // An example of profitability of using `forloop`
            // as we wanna pass all the children into
            // a linked expression like here
            var activeChildren = transform.
                Children().
                Where(child => child.gameObject.activeInHierarchy).
                ToList();
            
            transform.DisableChildren();
        }
        
    }
}