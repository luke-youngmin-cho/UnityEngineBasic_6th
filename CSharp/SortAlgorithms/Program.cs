using System;

namespace SortAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 4, 4, 2, 3, 5, 9, 7, 8, 6, 0 };
            //SortAlgorithms.BubbleSort(arr);
            //SortAlgorithms.SelectionSort(arr);
            SortAlgorithms.InsertionSort(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]}, ");
            }
        }
    }
}
