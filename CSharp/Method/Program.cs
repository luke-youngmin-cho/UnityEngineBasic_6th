using System;

// Function : 연산을 수행할 수 있는 기능
// Method : 객체/클래스 단위로 호출되는 Function (⊃ 멤버 함수)
// Method ⊂ Function
//
// 함수 형태 3가지
// 함수 선언, 정의, 호출
//
// 함수 선언 : 아래 형태와같은 함수를 사용하겠다 라고만 선언하는 것이고, 실제 함수가 어떤 연산을 하는지 구현부분이 없음.
// 반환형 함수이름 (파라미터);
//
// 함수 정의 :
// 반환형 함수이름 (파라미터)
// {
//     연산
//     return;
// }
// 
// 함수 호출 :
// 함수이름 (인자);

namespace Method
{
    class Program
    {
        // 전역변수 (글로벌변수)
        // 초기화값이 있을경우 데이터 영역에 할당, 초기화값이 없을경우 BSS 영역에 할당
        static bool _helloWorldPrinted;

        // 여기서 Main 함수는 함수 정의
        static void Main(string[] args)
        {
            // 지역변수 - 함수 내에서 선언된 변수
            // 지역변수는 함수 호출 스택에 같이 할당됨.
            // 해당 변수 공간에 어떤 데이터가 쓰여져 있는지 알 수 없기때문에 
            // 데이터를 대입한 후에 접근해야한다.
            bool somethingPrinted;
            somethingPrinted = false;
            Console.WriteLine(_helloWorldPrinted);
            Console.WriteLine(somethingPrinted);

            PrintHelloWorld();
            string name = "zㅣ존법사";
            PrintSomthing(name);
        }

        static void PrintHelloWorld()
        {
            // 여기 Console.WriteLine 은 함수 호출
            Console.WriteLine("Hello World!");
            _helloWorldPrinted = true;
        }

        static void PrintSomthing(string something)
        {
            Console.WriteLine(something);
        }
    }
}
