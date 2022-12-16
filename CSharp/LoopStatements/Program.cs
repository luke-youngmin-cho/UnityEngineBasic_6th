using System;

namespace LoopStatements
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // while 문
            // 형태
            //while (반복할조건)
            //{
            //  반복할내용
            //}
            // 조건이항상 참인 반복문 : 무한루프

            int[] arr = new int[100];
            int count = 0;
            while (count < 100)
            {
                arr[count] = count++;
                Console.WriteLine(arr[count - 1]);
            }

            // do while 문
            // 한번 일단 실행하고 반복조건 체크
            // do
            // {
            // 
            // } while (반복할 조건);

            do
            {
                count++;
                Console.WriteLine($"[Do-While] : {count}");
            } while (count < 100);

            // for 문
            //for (시작시 한번 수행할 내용; 반복할 조건; 반복할내용이 한번 끝날때 마다 수행할 내용)
            //{
            //
            //}

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            for (;;)
            {

            }

            string name = "김아무개";
            for (int i = 0; i < name.Length; i++)
            {
                Console.WriteLine(name[i]);
            }

            int[][] arr_arr_int = new int[3][];
            arr_arr_int[0] = new int[2];
            arr_arr_int[1] = new int[3];
            arr_arr_int[2] = new int[4];

            for (int i = 0; i < arr_arr_int.Length; i++)
            {
                for (int j = 0; j < arr_arr_int[i].Length; j++)
                {
                    arr_arr_int[i][j] = 1;
                }
            }
        }
    }
}
