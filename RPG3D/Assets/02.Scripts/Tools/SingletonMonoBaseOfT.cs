

using System;
using UnityEngine;

namespace RPG.Tools
{
    public abstract class SingletonMonoBase<T> : MonoBehaviour
        where T : SingletonMonoBase<T>
    {
        public static T instance
        {
            get
            {
                lock (_spinLock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                        _instance.Init();
                    }
                }
                
                return _instance;
            }
        }
        private static T _instance;
        private static object _spinLock = new object();

        protected virtual void Init()
        {
            UnityEngine.Debug.Log($"{typeof(T).Name} ÀÇ ¸ð³ë½Ì±ÛÅæ ÀÎ½ºÅÏ½º »ý¼ºµÊ.");
        }

        protected virtual void Awake()
        {
            lock (_spinLock)
            {
                if (_instance == null)
                {
                    _instance = (T)this;
                    _instance.Init();
                }
            }
        }
    }
}

