using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    internal class Monster
    {
        public int hp
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
                OnHPChange?.Invoke((float)_hp / _hpMax);
            }
        }
        private int _hp;
        private int _hpMax = 100;
        //public delegate void HPChangeHandler(float ratio);
        //public event HPChangeHandler OnHPChange;
        public event Action<float> OnHPChange;
        // event 한정자 : 해당 대리자의 호출을 선언된 클래스로 제한.
        // 외부 클래스에서는 이 대리자에 += (구독) -= (구독취소) 만 가능하다.

        public Monster()
        {
            hp = _hpMax;
        }
    }
}
