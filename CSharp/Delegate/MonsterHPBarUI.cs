using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    internal class MonsterHPBarUI
    {
        public float barValue; // 0.0f ~ 1.0f

        public MonsterHPBarUI(Monster owner)
        {
            owner.OnHPChange += Refresh;
        }

        public void Refresh(float value) => barValue = value;
    }
}
