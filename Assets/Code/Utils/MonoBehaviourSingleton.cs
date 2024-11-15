using UnityEngine;

namespace Code.Utils
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                
                var instance = FindObjectOfType<T>(true);
                    
                if (instance == null)
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    
                SetSingletonInstance(instance);
                return _instance;
            }
        }
        
        protected void Awake()
        {
            SetSingletonInstance(this as T);
        }

        protected virtual void Init() { }

        private static void SetSingletonInstance(T instance)
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(instance.gameObject);
                _instance = instance;
                _instance.Init();
            }
            else if (instance != _instance)
                Destroy(instance.gameObject);
        }
    }
}