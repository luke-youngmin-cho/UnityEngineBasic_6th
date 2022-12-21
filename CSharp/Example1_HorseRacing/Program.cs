using System;
using System.Threading;

// 진행 방식
//
// 말 클래스 필요
// 말 클래스는 달린거리, 이동하기(달리기) 라는 함수를 가집니다.
// 
// 프로그램 시작시
// 말 다섯마리 만들고
// 각 말은 초당 10 ~ 20 (정수형) 범위의 거리를 랜덤하게 전진.
// 각각의 말은 거리 200에 도달하면 도착해서 더이상 전진하지 않고 
// 매초 각 말들이 아직 달리고 있다면 달린 거리를, 도착했다면 도착 상태를 콘솔창에 출력 해줍니다.
// 모든 말이 도착했다면 경주를 끝내고 등수 순서대로 말들의 이름을 콘솔창에 출력 해줍니다.
namespace Example1_HorseRacing
{
    internal class Program
    { 
        class Horse
        {
            public string Name;
            public bool IsFinished;
            public int TotalDistance;

            public void Run(int distance)
            {
                TotalDistance += distance;
            }
        }

        static Random random;
        static int minSpeed = 10;
        static int maxSpeed = 20;
        static int goalPos = 200;

        static void Main(string[] args)
        {
            bool gameFinished = false;
            Horse[] horses = new Horse[5];
            Horse[] horsesFinished = new Horse[5];
            int grade = 0;
            int sec = 0;

            // 말 다섯마리 만들고 
            // 이름 다 설정 해줌 (이름 패턴은 경주마1, 경주마2, 경주마3 ... )
            for (int i = 0; i < horses.Length; i++)
            {
                horses[i] = new Horse();
                horses[i].Name = $"경주마{i + 1}";
            }

            Console.WriteLine("======================== 경주 시작 ============================");

            while (gameFinished == false)
            {             
                // 각 말은 초당 10 ~ 20 (정수형) 범위의 거리를 랜덤하게 전진.
                // 각각의 말은 거리 200에 도달하면 도착해서 더이상 전진하지 않고 
                // 매초 각 말들이 아직 달리고 있다면 달린 거리를, 도착했다면 도착 상태를 콘솔창에 출력 해줍니다.
                // 모든 말이 도착했으면 게임 끝냄
                for (int i = 0; i < horses.Length; i++)
                {
                    // 경주가 끝난 말
                    if (horses[i].IsFinished)
                    {
                        Console.WriteLine($"{horses[i].Name} 은 도착했다.");
                    }
                    // 경주가 아직 끝나지 않은말은 달리게한다.
                    else
                    {
                        random = new Random();
                        horses[i].Run(random.Next(minSpeed, maxSpeed + 1)); // 랜덤속도로 전진

                        Console.WriteLine($"{horses[i].Name} : 현재 거리 {horses[i].TotalDistance}");

                        // 전진했는데 도착했으면 끝났다고 해줌
                        if (horses[i].TotalDistance >= goalPos)
                        {
                            horses[i].IsFinished = true;
                            horsesFinished[grade++] = horses[i];
                        }
                    }
                }

                // 모든말이 도착했는지 체크
                if (grade >= horses.Length)
                    break;
                
                Thread.Sleep(1000);
                sec++;
                Console.WriteLine($"=================== {sec} 초 경과 ===================");
            }

            Console.WriteLine("======================== 경주 끝 ============================");
            // 각 말들을 등수 순서대로 이름을 출력해줌
            for (int i = 0; i < horsesFinished.Length; i++)
            {
                Console.WriteLine($"{i + 1} 등 : {horsesFinished[i].Name}");
            }
        }
    }
}
