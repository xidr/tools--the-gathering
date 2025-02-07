using UnityEngine;

namespace Tools.Extension_Methods
{
    public static class GameObjectExtensions
    {
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            return component == null ? component :  gameObject.AddComponent<T>();
        }
        
        // The Unity version of Null, not the standard C# Null
        // The object that is being marked for destruction
        // So we can use Null-Propagation operators `?.`, Null-Coalescing operators `??` safely on GO
        // Like we do it in Visitor
        public static T OrNull<T> (this T obj) where T : Object => obj ? obj : null;
        
        // Use transform extensions for GameObject
        public static void DestroyChildren(this GameObject gameObject)
        {
            gameObject.transform.DestroyChildren();
        }
    }
}