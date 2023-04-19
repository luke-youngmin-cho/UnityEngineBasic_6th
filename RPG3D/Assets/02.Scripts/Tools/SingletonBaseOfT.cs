using System;
using System.Reflection; 
// Reflection : 런타임중에 어셈블리에 접근하는 기능을 제공함.
// 특정 타입에대한 정보들이나 멤버들을 읽거나 새로운 타입 정의 등도 가능하다..
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Tools
{
    public abstract class SingletonBase<T>
        where T : SingletonBase<T>
    {
        public static T instance
        {
            get
            {
                lock (_spinLock)
                {
                    if (_instance == null)
                    {
                        // Activator : 조건에 부합하는 생성자를 탐색해서 호출하여 인스턴스를 생성하도록 도와주는 클래스
                        _instance = Activator.CreateInstance(typeof(T)) as T;

                        //ConstructorInfo constructorInfo = typeof(T).GetConstructor(new Type[] { });
                        //_instance = constructorInfo.Invoke(new object[] { }) as T;
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
            UnityEngine.Debug.Log($"{typeof(T).Name} 의 싱글톤 인스턴스 생성됨.");
        }
    }
}

