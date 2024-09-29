using UnityEngine;

namespace HikanyanLaboratory.Common
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        protected virtual bool UseDontDestroyOnLoad { get; } = false;
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance != null) return _instance;
                GameObject gameObj = new GameObject();
                gameObj.name = typeof(T).Name;

                _instance = gameObj.AddComponent<T>();
                if ((_instance as SingletonMonoBehaviour<T>)!.UseDontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObj);
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (UseDontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}