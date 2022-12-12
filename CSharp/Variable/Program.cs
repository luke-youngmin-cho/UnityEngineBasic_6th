using System;

// 변수 
// 아직 정해지지 않은 값

// 변수를 사용하겠다고 '선언' 할때는 
// 해당 변수의 자료형(타입/형태) 를 알려줘야한다.

// bit : 한자리 이진수 (0 과 1로 표현, 정보처리의 최소단위)
// byte : 8 bit (데이터 처리의 최소단위)
namespace Variable
{
    internal class Program
    {
        // 변수 형태:
        // 자료형 변수이름 
        // 초기값을 주고싶으면 
        // 자료형 변수이름 대입연산자 값
        // 변수 선언의 의미 : 해당 자료형 만큼의 메모리공간을 확보하겠다.

        // int : 부호가 있는 32-bit 정수형
        int index = 7;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    class Human
    {
        int age = 1; // 4 byte 부호있는 정수형
        float height = 20.0f; // 4byte 실수형
        double weight = 20.0; // 8byte 실수형
        bool isResting = true; // 1byte 논리형
        // true : 0 이 아닌 값, false : 0
        char gender = 'W'; // 2byte 문자형 (ASCII 코드표를 따름)
        string name = "아무개"; // 문자열형, 문자갯수 * 2byte + 1byte (null)
        // c 계통언어에서는 모두 문자열의 끝을 인식하기위해 null byte 를 사용함.
    }
}
