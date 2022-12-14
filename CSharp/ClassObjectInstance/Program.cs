using System;

namespace ClassObjectInstance
{
    class Program
    {
        static void Main(string[] args)
        {
            // new 키워드 : 동적 할당 키워드
            Human human = new Human();
            // L-value : Human 클래스 타입의 참조 지역 변수
            // R-value : Human 객체 생성후 반환된 객체 참조

            // .연산자 (멤버접근연산자)
            human.SayAge();
            human.age = 2;
            human.SayAge();

            // Class : 
            // 객체 타입 

            // Object :
            // 객체 : Class 타입으로 메모리공간을 확보한 것

            // Instance :
            // 객체(데이터가 할당된 경우)

            // Instance ⊂ Object
        }
    }

    class Human
    {
        // 접근제한자
        // public : 다른 객체/클래스가 접근 가능
        // protected : 상속받은 자식 객체만 접근 가능
        // private : 다른 객체/클래스 접근 불가능
        // internal : 동일 어셈블리 내에서 다른 객체가 접근 가능
        // 캡슐화 : 멤버들에 보호처리 / 연산처리 등을 추가 해주기위한 과정
        public int age; 
        private float height = 20.0f;
        private double weight = 20.0;
        private bool isResting = true;
        private char gender = 'W';
        private string name = "아무개";

        // C# 에는 두가지 데이터 접근 형식이 있음
        // 값 타입, 참조 타입
        // 값 타입 : 할당된 메모리의 데이터를 직접 쓰고 읽는 형태
        // 참조 타입 : 할당된 메모리의 주소를 통해서 간접적으로 데이터를 쓰고 읽는 형태

        // 생성자
        // 해당 클래스타입의 객체를 힙영역에다가 할당한 다음 해당 영역의 참조를 반환하는 함수        
        public Human()
        {

        }

        // 소멸자
        // 힙영역에 할당되었던 객체를 메모리에서 해제하기 위해 호출하는 함수
        ~Human()
        {

        }


        public void SayAge()
        {
            Console.WriteLine(age);
        }

        public void SayHeight()
        {
            Console.WriteLine(height);
        }

        public void SayName()
        {
            Console.WriteLine(name);
        }
    }
}
