using UnityEngine;

namespace ioc.IOCStudents.Core
{
    /// <summary>
    /// Persistent Singleton pattern.
    /// </summary>
    /// <typeparam name="T">Instantiation type of this singleton </typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : PersistentSingleton<T>
    {
        /// <summary>
        /// Singleton design pattern
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get { return ((T)SingletonInstance); }
            set { SingletonInstance = value; }
        }
        private static T m_instance;
        protected bool m_enabled;


        protected static PersistentSingleton<T> SingletonInstance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<T>();
                    if (m_instance == null)
                    {
                        GameObject obj = new GameObject();
                        m_instance = obj.AddComponent<T>();
                    }
                }
                return m_instance;
            }

            set
            {
                m_instance = value as T;
            }
        }

        /// <summary>
        /// On awake, we initialize our instance. If you need to use Awake, make sure to override it, and call base.Awake()
        /// </summary>
        protected virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (m_instance == null)
            {
                //If I am the first instance, make me the Singleton
                m_instance = this as T;
                DontDestroyOnLoad(transform.gameObject);
                m_enabled = true;
            }
            else
            {
                //If a Singleton already exists and you find
                //another reference in scene, destroy it!
                if (this != m_instance)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

