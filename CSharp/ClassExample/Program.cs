using System;

namespace ClassExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Human human1 = new Human();
            human1.Age = 100;
            human1.Height = 200.0f;
            human1.Gender = '남';

            Human human2 = new Human();
            human2.Age = 50;
            human2.Height = 150.0f;
            human2.Gender = '여';

            human1.SayAge();
            human2.SayAge();
        }
    }

    class Human
    {
        public int Age;
        public float Height;
        public char Gender;


        public void SayAge()
        {
            Console.WriteLine(Age);
        }
    }
}
