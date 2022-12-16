using System;

namespace Array
{
    // 배열 
    // 동일한 자료형으로 연속적인 데이터 타입 (연속적인 : 메모리상에서 주소가연속적으로 붙어있다)
    // 형태 : 
    // 자료형[] 변수이름
    internal class Program
    {
        static void Main(string[] args)
        {
            // R-Value : 힙영역에 크기 4Byte * 3 의 배열을 할당하고 해당 영역 참조 반환
            // L-Value : 배열 참조를 대입할 배열참조타입 지역변수
            int[] arr_int = new int[3];

            // 인덱서 []
            // 인덱스접근용 연산자 
            // 해당 타입크기 * 인덱스 뒤의 주소에 참조하는 연산자
            // 호출방법:
            // 변수이름[인덱스]
            arr_int[0] = 1;
            arr_int[1] = 2;
            arr_int[2] = 3;
            int[] arr_int2 = {1, 2, 3};
            float[] arr_float = { 1.0f, 2.0f, 3.0f };

            int[][] arr_arr_int = new int[3][];
            arr_arr_int[0] = new int[3];
            arr_arr_int[1] = new int[2];
            arr_arr_int[2] = new int[4];

            arr_arr_int[0][1] = 1;
            string name = "김아무개";
            char first = name[0];
            Console.WriteLine(name[0]);
            char[] arr_char = { '김', '아', '무', '개', '\0' };
        }
    }

}
