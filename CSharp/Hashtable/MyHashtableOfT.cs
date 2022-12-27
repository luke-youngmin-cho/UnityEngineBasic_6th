using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtable
{
    internal class MyHashtable<TKey, TValue>
    {
        private const int DEFAULT_SIZE = 100;
        private TValue[] _data = new TValue[DEFAULT_SIZE];
        public TValue this[TKey key]
        {
            get
            {
                return _data[Hash(key)];
            }
            set
            {
                _data[Hash(key)] = value;
            }
        }
        public void Add(TKey key, TValue value)
        {
            _data[Hash(key)] = value;
        }

        public bool ContainsKey(TKey key)
        {
            return Comparer<TValue>.Default.Compare(_data[Hash(key)], default(TValue)) == 0;
        }

        public bool ContainsValue(TValue value)
        {
            for (int i = 0; i < _data.Length; i++)
            {
                if (Comparer<TValue>.Default.Compare(_data[i], value) == 0)
                    return true;
            }

            return false;
        }

        // out 키워드 
        // 파라미터 와 인자 앞에 붙어서 
        // 해당 함수가 리턴할 때 결정된 값을 인자에다가 대입해준다.
        public bool TryGetValue(TKey key, out TValue value)
        {
            // try- catch- finally 구문 
            // 예외잡는 구문 
            // try 내에서 연산중에 예외가 던져지면 
            // catch 문 실행 
            // try- catch 문이 끝나면 finally 구문 실행 (finally 구문 안써도됨) 
            bool success = false;
            value = default(TValue);
            try
            {
                value = _data[Hash(key)];
                success = true;
            }
            catch (Exception e)
            {
                success = false;
                Console.WriteLine($"Invalid key {e.Message}");
                throw new Exception("adsfasdf");
                throw e;
            }
            finally
            {
            }

            return success;
        }

        private int Hash(TKey key)
        {
            string keyName = key.ToString();
            int sum = 0;
            for (int i = 0; i < keyName.Length; i++)
            {
                sum += keyName[i];
            }

            return sum % _data.Length;
        }
    }
}
