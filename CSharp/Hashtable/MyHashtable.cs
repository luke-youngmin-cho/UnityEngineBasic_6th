using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtable
{
    internal class MyHashtable
    {
        private const int DEFAULT_SIZE = 100;
        private object[] _data = new object[DEFAULT_SIZE];

        public object this[object key]
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

        public void Add(object key, object value)
        {
            _data[Hash(key)] = value;
        }

        public bool ContainsKey(object key)
        {
            return Comparer<object>.Default.Compare(_data[Hash(key)], default(object)) == 0;
        }

        public bool ContainsValue(object value)
        {
            for (int i = 0; i < _data.Length; i++)
            {
                if (Comparer<object>.Default.Compare(_data[i], value) == 0)
                    return true;
            }

            return false;
        }

        // out 키워드 
        // 파라미터 와 인자 앞에 붙어서 
        // 해당 함수가 리턴할 때 결정된 값을 인자에다가 대입해준다.
        public bool TryGetValue(object key, out object value)
        {
            // try- catch- finally 구문 
            // 예외잡는 구문 
            // try 내에서 연산중에 예외가 던져지면 
            // catch 문 실행 
            // try- catch 문이 끝나면 finally 구문 실행 (finally 구문 안써도됨) 
            bool success = false;
            value = default(object);
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

        private int Hash(object key)
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
