using System;

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

            MyDynamicArray<double>.MyDynamicArrayEnum<double> enumerator
                = da_double.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            enumerator.Reset();
        }

        public static bool BiggerThan20(int value)
        {
            return value > 20;
        }
    }
}
