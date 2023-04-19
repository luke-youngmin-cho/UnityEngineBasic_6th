using System;
using System.Reflection; 
// Reflection : ��Ÿ���߿� ������� �����ϴ� ����� ������.
// Ư�� Ÿ�Կ����� �������̳� ������� �аų� ���ο� Ÿ�� ���� � �����ϴ�..
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
                        // Activator : ���ǿ� �����ϴ� �����ڸ� Ž���ؼ� ȣ���Ͽ� �ν��Ͻ��� �����ϵ��� �����ִ� Ŭ����
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
            UnityEngine.Debug.Log($"{typeof(T).Name} �� �̱��� �ν��Ͻ� ������.");
        }
    }
}

