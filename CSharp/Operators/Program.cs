using System;

namespace Operators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 14;
            int b = 5;
            int c = 0;

            // 산술 연산
            // 더하기, 빼기, 나누기, 곱하기, 나머지
            //==================================================================

            // 더하기
            c = a + b;
            Console.WriteLine(c);

            // 빼기
            c = a - b;
            Console.WriteLine(c);

            // 나누기 - 정수연산시 나머지 버림
            c = a / b;
            Console.WriteLine(c);

            // 곱하기
            c = a * b;
            Console.WriteLine(c);

            // 나머지 - 실수 연산시 정수연산과 동일함
            c = a % b;
            Console.WriteLine(c);

            // 증감 연산
            // 증가, 감소 연산자
            //==================================================================

            // 증가
            ++c; // 전위연산 // c = c + 1;
            c++; // 후위연산

            c = 0;
            Console.WriteLine(++c); // result : 1
            Console.WriteLine(c++); // result : 0

            // 감소
            --c; // c = c - 1;
            c--;

            // 관계 연산
            // 같음, 다름, 크기 비교 연산
            //==================================================================

            // 같음
            Console.WriteLine(a == b);

            // 다름
            Console.WriteLine(a != b);

            // 크다
            Console.WriteLine(a > b);

            // 작다
            Console.WriteLine(a < b);

            // 크거나 같다
            Console.WriteLine(a >= b);

            // 작거나 같다
            Console.WriteLine(a <= b);

            // 대입 연산
            // (더해서, 빼서, 나누어서, 곱해서, 나머지구해서) 대입
            //==================================================================

            c = 20;
            Console.WriteLine($"C value = {c}");
            c += b;
            Console.WriteLine(c);
            c -= b;
            Console.WriteLine(c);
            c /= b;
            Console.WriteLine(c);
            c *= b;
            Console.WriteLine(c);
            c %= b;
            Console.WriteLine(c);

            // 논리 연산
            // 논리형의 피연산자들을 대상으로 연산수행
            // or, and, xor, not
            //==================================================================

            bool A = true;
            bool B = false;

            // or
            // A 와 B 둘중 하나라도 true 이면 true 반환, 나머지경우 false 반환
            Console.WriteLine(A | B);

            // and
            // A 와 B 둘 다 true 이면 true 반환 ,나머지경우 false 반환
            Console.WriteLine(A & B);

            // xor
            // A 와 B 둘중 하나만 true 일때 true 반환, 나머지경우 false 반환
            Console.WriteLine(A ^ B);

            // not
            // 피연산자 값을 반전
            Console.WriteLine(!A);

            // 조건부 논리 연산
            // 왼쪽 피연산자에 따라서 오른쪽 피연산자와 비교연산을 수행할지 말지 평가한 후에 연산함.
            //==================================================================

            // or 
            // A 가 true 일 경우 B 의 값에 상관없이 무조건 결과는 true 이므로 A 를 그대로 반환
            Console.WriteLine(A || B);

            // and
            // A 가 false 일 경우 B 의 값에 상관없이 무조건 결과는 false 이므로 A 를 그대로 반환
            Console.WriteLine(A && B);

            // 비트 연산
            // 정수형에 대해서만 수행함
            //==================================================================

            // or
            Console.WriteLine(a | b);

            // and
            Console.WriteLine(a & b);

            // xor
            Console.WriteLine(a ^ b);

            // not
            Console.WriteLine(~a);

            // shift - left
            Console.WriteLine(a << 1);

            // shift - right
            Console.WriteLine(a >> 1);
        }
    }
}
