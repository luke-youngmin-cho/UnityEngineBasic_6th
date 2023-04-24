using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RPG.DataModels
{
    public struct ItemPair
    {
        public static ItemPair empty = new ItemPair(-1, 0);
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

    public class InventoryDataModel : CollectionDataModelBase<ItemPair>
    {
        private string _path;

        public InventoryDataModel()
        {
            _path = Application.persistentDataPath + "InventoryData.json";
        }

        new public void Add(ItemPair item)
        {
            // 동일한 아이템이 이미 존재할때 갯수증가 가능여부확인
            int validIndex = FindIndex(x => x.id == item.id);
            if (validIndex >= 0)
            {
                Set(validIndex, new ItemPair(item.id, Items[validIndex].num + item.num));
                return;
            }

            // 빈 슬롯 있는지 확인
            int emptyIndex = FindIndex(x => x == ItemPair.empty);
            if (emptyIndex >= 0)
            {
                Set(validIndex, item);
                return;
            }
        }

        new public void Remove(ItemPair item)
        {
            int validIndex = FindIndex(x => x.id == item.id);
            if (validIndex >= 0)
            {
                if (Items[validIndex].num > item.num)
                {
                    Set(validIndex, new ItemPair(item.id, Items[validIndex].num - item.num));
                }
                else if (Items[validIndex].num == item.num)
                {
                    Set(validIndex, ItemPair.empty);
                }
                else
                {
                    throw new System.Exception($"[InventoryDataModel] : Failed to remove item. Number exceeded");
                }
            }
        }

        public override bool Load()
        {
            InventoryDataModel tmp;
            if (File.Exists(_path) == false)
            {
                Save();
            }
            tmp = JsonUtility.FromJson<InventoryDataModel>(File.ReadAllText(_path));

            foreach (var item in tmp.Items)
            {
                this.Items.Add(item);
            }
            return true;
        }

        public override bool Save()
        {
            File.WriteAllText(_path, JsonUtility.ToJson(this));
            return true;
        }
    }
}
