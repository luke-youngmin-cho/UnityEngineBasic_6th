using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithms
{
    internal static class SortAlgorithms
    {
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

        private static void Swap(ref int a, ref int b)
        {
            int tmp = b;
            b = a;
            a = tmp;
        }
    }
}
