using System;

// 구조체 사용자 정의 자료형
// 멤버들을 캡슐화하는 자료형 
// 멤버 변수, 프로퍼티, 함수 .. 
//
// 선언 형태 
// struct 이름
// {
//   멤버들 선언/정의
// }

namespace Structure
{
    public struct Stats
    {        
        // 프로퍼티 
        // getter / setter (get 접근자 / set 접근자) 를 구현할 수 있는 속성
        public int STR
        {
            get
            {
                return _str;
            }
            set
            {
                if (value < 0)
                    return;

                _str = value;
            }
        }

        public  static Stats Zero
        {
            get
            {
                return new Stats(0, 0, 0, 0);
            }
        }
        public static Stats Default => new Stats(10, 10, 10, 10);

        private int _str;
        private int _dex;
        private int _int;
        private int _luk;

        // 오버로드 (OverLoad)
        // 매개변수가 다르면서 동일한 이름의 함수를 정의할 수 있는 기능

        // 생성자 오버로딩
        // 구조체의 생성자에서는 모든 멤버를 초기화 해야함.
        public Stats(int STR, int DEX, int INT, int LUK)
        {
            this._str = STR;
            this._dex = DEX;
            this._int = INT;
            this._luk = LUK;
        }


        public static Stats GetDefault()
        {
            return new Stats(10, 10, 10, 10);
        }

        public static Stats GetZero()
        {
            return new Stats(0, 0, 0, 0);
        }

        public void SetSTR(int value)
        {
            if (value < 0)
                return;

            this._str = value;
        }

        public int GetSTR()
        {
            return this._str;
        }

        public void SetDEX(int value)
        {
            if (value < 0)
                return;

            this._dex = value;
        }

        public int GetDEX()
        {
            return this._dex;
        }

        public void SetINT(int value)
        {
            if (value < 0)
                return;

            this._int = value;
        }

        public int GetINT()
        {
            return this._int;
        }

        public void SetLUK(int value)
        {
            if (value < 0)
                return;

            this._luk = value;
        }

        public int GetLUK()
        {
            return this._luk;
        }

        public int GetCombatPower()
        {
            return _str + _dex + _int + _luk;
        }

        public int GetCombatPower(int gain)
        {
            return GetCombatPower() * gain;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 클래스는 참조타입
            Player player = new Player();
            player.STR = 10;
            player.DEX = 10;
            player.INT = 10;
            player.LUK = 10;

            // 구조체는 값타입
            Player_s player_s = new Player_s();
            player_s.STR = 10;
            player_s.DEX = 10;
            player_s.INT = 10;
            player_s.LUK = 10;

            // 그러면은 언제 클래스를 쓰고 언제 구조체를 쓰나?
            // 1. 멤버들의 크기 총합이 16 byte 초과일때는 일반적으로 클래스를 씀.
            // 
            // 참조타입으로 값을 읽고 쓰는거 보다 값타입을 읽고 쓰는게 빠르다. 
            // 하지만 16 byte를 초과할때는 레지스터가 두번 이상 값을 읽어야 하기 때문에 
            // 값타입이어도 참조타입보다 느려진다.
            //
            // 2. 값을 쓰고 읽는것이 빈번하게 일어날 경우 (ex. 함수의 인자로 넘겨주는 횟수가 잦을 경우)
            // 구조체를 사용하는것이 효율적이다. 
            Enemy enemy = new Enemy();
            enemy.Stats.SetSTR(10);
            enemy.Stats.SetDEX(10);
            enemy.Stats.SetINT(10);
            enemy.Stats.SetLUK(10);
            enemy.Stats.STR = 10;
            Console.WriteLine(enemy.Stats.STR);
            Console.WriteLine($"현재 적의 힘 : {enemy.Stats.GetSTR()}");
            enemy.Stats = new Stats(10, 10, 10, 10);
            enemy.Stats = Stats.GetDefault();
            Console.WriteLine($"적의 전투력이 {enemy.Stats.GetCombatPower()} 입니다 !!");

        }
    }

    public class Player
    {
        public int STR;
        public int DEX;
        public int INT;
        public int LUK;
    }
    public struct Player_s
    {
        public int STR;
        public int DEX;
        public int INT;
        public int LUK;
    }

    public class Enemy
    {
        public Stats Stats;
    }
}
