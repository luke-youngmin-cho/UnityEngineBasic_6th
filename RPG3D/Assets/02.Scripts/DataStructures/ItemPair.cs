using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.DataStructures
{
    [Serializable]
    public struct ItemPair
    {
        public static ItemPair empty = new ItemPair(-1, 0);
        public static ItemPair error = new ItemPair(0, 0);
        public int id;
        public int num;

        public ItemPair(int id, int num)
        {
            this.id = id;
            this.num = num;
        }

        public static bool operator ==(ItemPair op1, ItemPair op2)
            => (op1.id == op2.id) && (op1.num == op2.num);

        public static bool operator !=(ItemPair op1, ItemPair op2)
            => !(op1 == op2);
    }
}
