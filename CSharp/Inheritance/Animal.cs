using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // abstract 
    // 추상화 키워드 
    // 멤버 앞에 붙어서 해당 멤버를 추상화시킴
    // (구현부 없이 선언만 하고 해당 추상화클래스를 상속받은 자식이 반드시 구현을 해야한다)
    internal abstract class Animal : IAttackable
    {
        public static string Species = string.Empty;

        public abstract void Breath();

        // virtual
        // 가상키워드 
        // 추상화용 키워드이며, 기본 구현을 할 수 있고, 그 구현을 다시 자식클래스에서 재정의 할 수 있다.
        public virtual void Attack()
        {
            Console.WriteLine($"동물 {Species} 가 공격했다.");
        }
    }
}
