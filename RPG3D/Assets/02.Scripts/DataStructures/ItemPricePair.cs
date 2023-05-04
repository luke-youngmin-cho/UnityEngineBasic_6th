using System;

namespace RPG.DataStructures
{
    [Serializable]
    public class ItemPricePair
    {
        public int id;
        public Gold price;

        public ItemPricePair(int id, Gold price)
        {
            this.id = id;
            this.price = price;
        }
    }
}
