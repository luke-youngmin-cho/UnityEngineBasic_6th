using System;

// Delegate : 대리자 
// Methods 에 대한 참조를 나타내는 형식
namespace Delegate
{
    internal class Program
    {
        // 대리자 타입 선언
        public delegate int OPHandler(int a, int b);
        public static OPHandler op;

        static void Main(string[] args)
        {           
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
