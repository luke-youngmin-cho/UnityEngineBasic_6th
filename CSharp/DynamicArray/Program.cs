using System;
using System.Collections;
using System.Collections.Generic;

namespace DynamicArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[3];
            arr[0] = 1;
            int a = arr[0];
            MyDynamicArray da = new MyDynamicArray();
            da.Add(1);
            Console.WriteLine(da[0]);
            da.Find(BiggerThan20);

            MyDynamicArray<double> da_double = new MyDynamicArray<double>();
            da_double.Add(3.0f);
            da_double.Add(6.5f);
            da_double.Add(4.2f);

            IEnumerator<double> enumerator = da_double.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            enumerator.Reset();
            enumerator.Dispose();

            // using : Dispose() 호출을 보장하는 구문. IDisposable 을 상속받은 객체만 인자로 줄 수 있다.
            MyDynamicArray<int> da_int = new MyDynamicArray<int>();
            using (IEnumerator<int> enumerator_int = da_int.GetEnumerator())
            {
                while (enumerator_int.MoveNext())
                    Console.WriteLine(enumerator_int.Current);
                enumerator.Reset();
            }


            foreach (var item in da_double)
            {
                Console.WriteLine(item);
            }
        }

        public static bool BiggerThan20(int value)
        {
            return value > 20;
        }
    }
}
