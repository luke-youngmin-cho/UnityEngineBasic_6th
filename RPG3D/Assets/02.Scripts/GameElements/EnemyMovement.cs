using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameElements
{
    public class EnemyMovement : MovementBase
    {
        public override float v => _v;
        private float _v;

        public override float h => _h;
        private float _h;

        public override float gain => _gain;
        private float _gain;

        public override void SetMove(float horizontal, float vertical, float gain)
        {
            base.SetMove(horizontal, vertical, gain);
            _h = horizontal;
            _v = vertical;
            _gain = gain;
        }
    }
}