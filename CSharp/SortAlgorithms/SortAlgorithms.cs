using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithms
{
    internal static class SortAlgorithms
    {
        #region Bubble Sort
        /// <summary>
        /// 거품 정렬
        /// 바로뒤의 요소가 현재요소보다 작으면 스왑 
        /// Outer For loop 한번 돌 때마다 가장 큰 수의 위치가 하나씩 고정 
        /// O(N^2)
        /// Stable : 값이 동일한 요소들을 정렬한 후에 기존 순서가 보장됨
        /// </summary>
        public static void BubbleSort(int[] arr)
        {
            int i, j;
            for (i = 0; i < arr.Length - 1; i++)
            {
                for (j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                    }
                }
            }
        }
        #endregion

        #region Selection Sort
        /// <summary>
        /// 선택 정렬
        /// 현재의 바로뒤부터 끝까지 중에서 가장 작은 요소를 찾아서 스왑
        /// Outer For loop 한번 돌 때마다 가장 작은 수의 위치가 하나씩 고정 
        /// O(N^2)
        /// Unstable
        /// </summary>
        /// <param name="arr"></param>
        public static void SelectionSort(int[] arr)
        {
            int i, j, min;
            for (i = 0; i < arr.Length; i++)
            {
                min = i;
                // 가장 작은 요소의 인덱스 찾기
                for (j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[min])
                        min = j;
                }

                Swap(ref arr[i], ref arr[min]);
            }
        }
        #endregion

        #region Insertion Sort
        /// <summary>
        /// 삽입 정렬
        /// 현재위치보다 이전 위치들중에서 더 큰값이 있으면 더 큰값으로 현재 위치에 덮어쓰고 
        /// 덮어쓰기 끝나면 마지막으로 덮어쓸때 참조했던 위치에 기존 현재위치의 값을 덮어씀 (스왑)
        /// O(N^2)
        /// Stable
        /// </summary>
        /// <param name="arr"></param>
        public static void InsertionSort(int[] arr)
        {
            int i, j, key;
            for (i = 1; i < arr.Length; i++)
            {
                key = arr[i];
                j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }
        #endregion

        #region Merge Sort
        /// <summary>
        /// 병합 정렬
        /// 요소를 최소단위까지 나눈 후에 차례대로 병합을 하면서 정렬함. (Divide and conquer)
        /// O(NLogN)
        /// Stable
        /// </summary>
        /// <param name="arr"></param>
        public static void MergeSort(int[] arr)
        {
            MergeSort(arr, 0, arr.Length - 1);
        }

        public static void MergeSort(int[] arr, int start, int end)
        {
            if (start < end)
            {
                int mid = end + (start - end) / 2 - 1; // overflow 방지용  == (start + end) / 2
                MergeSort(arr, start, mid);
                MergeSort(arr, mid + 1, end);

                Merge(arr, start, mid, end);
            }
        }

        private static void Merge(int[] arr, int start, int mid, int end)
        {
            int[] tmp = new int[end + 1];
            for (int i = 0; i < end + 1; i++)
                tmp[i] = arr[i];

            int part1 = start;
            int part2 = mid + 1;
            int index = start;

            // part1 과 part2 비교해서 정렬하면서 채워넣음. 
            while (part1 <= mid && part2 <= end)
            {
                if (tmp[part1] <= tmp[part2])
                {
                    arr[index++] = tmp[part1++];
                }
                else
                {
                    arr[index++] = tmp[part2++];
                }
            }

            // 남은 Part1 들을 index 위치 뒤에 쭉 이어 붙여준다. 
            // 정복이 끝나고 남은 Part2는 원본 배열 그대로 남아있으면 타당한 위치기때문에 따로 신경쓸 필요 없다.
            for (int i = 0; i <= mid - part1; i++)
            {
                arr[index + i] = tmp[part1 + i];
            }
        }

        #endregion

        /// <summary>
        /// 퀵 정렬
        /// O(N^2)
        /// θ(NLogN)
        /// </summary>
        /// <param name="arr"></param>
        #region Quick Sort

        public static void QuickSort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
        }

        public static void QuickSort(int[] arr, int start, int end)
        {
            if (start < end)
            {
                int p = Partition(arr, start, end);
                QuickSort(arr, start, p - 1);
                QuickSort(arr, p + 1, end);
            }
        }

        private static int Partition(int[] arr, int start, int end)
        {
            int pivot = arr[end + (start - end) / 2];

            while (true)
            {
                while (arr[start] < pivot) start++;
                while (arr[end] > pivot) end--;

                if (start < end)
                {
                    Swap(ref arr[start], ref arr[end]);
                }
                else
                {
                    return end; // returnm pivot index
                }
            }
        }

        #endregion

        private static void Swap(ref int a, ref int b)
        {
            int tmp = b;
            b = a;
            a = tmp;
        }
    }
}
