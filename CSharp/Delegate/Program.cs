using System;
using System.Collections.Generic;

// Delegate : 대리자 
// Methods 에 대한 참조를 나타내는 형식
namespace Delegate
{
    internal class Program
    {
        // 대리자 타입 선언
        public delegate int OPHandler(int a, int b);
        public static OPHandler op;

        // C# 기본 대리자들
        public Action action;
        public Action<int> action2;
        public Action<float, float> action3;
        public Func<int, int, int> op2;
        public Predicate<int> predicate;


        static void Main(string[] args)
        {
            Monster monster = new Monster();
            MonsterHPBarUI monsterHPBarUI = new MonsterHPBarUI(monster);
            
            
            while (true)
            {
            }

            //===============================================================
            int result = 0;
            Sum(1, 2);
            Sub(1, 2);
            Div(1, 2);
            Mul(1, 2);

            op += Sum;   
            op += Sub;
            op += Div;
            op += Mul;

            op(1, 2); 
            op.Invoke(1, 2); // 일반호출과 Invoke호출의 차이는 멀티쓰레드환경에서 레이스컨디션을 방지하기위한 함수
            op?.Invoke(1, 2); // ? : null check 연산자. null 이면 호출안함

            // 익명 대리자 추가 형태
            op += delegate (int a, int b)
            {
                return a + b + b + a;
            };

            // 익명 메소드 추가 형태 (람다식)
            // 굳이 명시하지않아도 컴파일러가 판단할수 있는 내용들을 전부 생략하고 => 로 람다식표현이라는것만 명시하는 형태로 사용
            op += (a, b) =>
            {
                return a + b + b + a;
            };

        }

        static int Sum(int a, int b)
        {
            Console.WriteLine(a + b);
            return a + b;
        }
        static int Sub(int a, int b)
        {
            Console.WriteLine(a - b);
            return a - b;
        }
        static int Div(int a, int b)
        {
            Console.WriteLine(a / b);
            return a / b;
        }
        static int Mul(int a, int b)
        {
            Console.WriteLine(a * b);
            return a * b;
        }
    }
}
