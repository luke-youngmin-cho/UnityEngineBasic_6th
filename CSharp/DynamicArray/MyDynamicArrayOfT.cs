using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicArray
{
    // Generic (제네릭)
    // 자료형을 일반화하는 문법
    // 클래스 / 구조체 / 인터페이스 / 함수 등의 이름뒤에 붙어서 정해지지 않은 타입에 대한 일반식을 
    // 정의할 때 사용한다.
    internal class MyDynamicArray<T>
    {        
        // const 키워드 
        // 상수 키워드. const 키워드가 붙은 변수는 
        // 초기화만 가능하며, 상수처럼 사용된다.
        private const int DEFAULT_SIZE = 1;
        private T[] _data = new T[DEFAULT_SIZE];
        public int Count;
        public int Capacity
        {
            get
            {
                return _data.Length;
            }
        }
        public T this[int index]
        {
            get
            {
                return _data[index];
            }
            set
            {
                _data[index] = value;                
            }
        }

        // 삽입 알고리즘
        // 일반적으로 O(1)
        // 단, Capacity가 모자랄때는 O(N)
        public void Add(T item)
        {
            if (Count >= Capacity)
            {
                T[] tmp = new T[(int)Math.Ceiling(Math.Log10(Capacity)) + 1];
                for (int i = 0; i < Count; i++)
                {
                    tmp[i] = _data[i];
                }
                _data = tmp;
            }

            _data[Count++] = item;
        }

        // 탐색 알고리즘
        // O(N)
        public bool Contains(T target)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Comparer<T>.Default.Compare(_data[i], target) == 0)
                    return true;
            }

            return false;
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < Count; i++)
            {
                if (match(_data[i]))
                    return _data[i];
            }

            // default 키워드
            // 해당 타입의 고정값을 반환하는 키워드
            return default(T);
        }

        // 삭제 알고리즘
        // O(N)
        public bool Remove(T item)
        {
            int index = -1;
            for (int i = 0; i < Count; i++)
            {
                if (Comparer<T>.Default.Compare(_data[i], item) == 0)
                {
                    index = i;
                    break;
                }
            }

            for (int i = index; i < Count; i++)
            {
                _data[i] = _data[i + 1];
            }

            Count--;
            return index >= 0;
        }

        // 인덱스 삭제 알고리즘
        // O(N)
        public bool RemoveAt(int index)
        {
            if (index > Count - 1)
                return false;

            for (int i = index; i < Count; i++)
            {
                _data[i] = _data[i + 1];
            }
            Count--;
            return true;
        }

        public void Clear()
        {
            //_data = new int[DEFAULT_SIZE];

            for (int i = 0; i < Capacity; i++)
            {
                _data[i] = default(T);
            }
        }
    }
}
