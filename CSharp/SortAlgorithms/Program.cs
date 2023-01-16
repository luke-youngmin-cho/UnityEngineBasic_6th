using System;
using System.Diagnostics;
using System.Linq;

namespace SortAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Random random = new Random();

            int[] arr = { 1, 4, 2, 3, 5, 9, 7, 8, 6, 0 };
            //SortAlgorithms.BubbleSort(arr);
            //SortAlgorithms.SelectionSort(arr);
            //SortAlgorithms.InsertionSort(arr);
            //SortAlgorithms.MergeSort(arr);
            //SortAlgorithms.QuickSort(arr);
            arr = Enumerable
                        .Repeat(0, 10000000)
                        .Select(i => random.Next(0, 10000000))
                        .ToArray();
            SortAlgorithms.HeapSort(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                //Console.Write($"{arr[i]}, ");
            }


            stopWatch.Stop();
            Console.WriteLine($"소요시간 : {stopWatch.ElapsedMilliseconds}");
        }
    }
}
