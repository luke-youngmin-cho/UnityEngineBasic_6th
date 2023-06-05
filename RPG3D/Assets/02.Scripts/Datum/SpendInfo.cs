using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Datum
{
    [CreateAssetMenu(fileName = "new SpendInfo", menuName = "RPG/Create a new SpendInfo")]
    public class SpendInfo : ItemInfo
    {
        public float hpRecoveringAmount;
        public float mpRecoveringAmount;

        public override void Use()
        {
            throw new NotImplementedException();
        }
    }
}
