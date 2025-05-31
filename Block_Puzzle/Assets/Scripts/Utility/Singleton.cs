using UnityEngine;

namespace Cublocks
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = default(T);
        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = FindFirstObjectByType(typeof(T)) as T;
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null) instance = GetComponent<T>();
            else Destroy(gameObject);
        }
    } 
}
