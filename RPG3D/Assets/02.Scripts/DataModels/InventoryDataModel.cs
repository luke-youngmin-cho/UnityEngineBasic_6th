using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using RPG.DataStructures;

namespace RPG.DataModels
{
    public class InventoryDataModel : CollectionDataModelBase<ItemPair>
    {
        private string _path;

        [Serializable]
        public class Data
        {
            public List<ItemPair> items;

            public Data(IEnumerable<ItemPair> copy)
            {
                items = new List<ItemPair>(copy);
            }
        }


        public InventoryDataModel()
        {
            _path = Application.persistentDataPath + "/InventoryData.json";
            Load();
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
                Set(emptyIndex, item);
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
            if (File.Exists(_path) == false)
            {
                CreateDefaultData();
            }

            Data data = JsonUtility.FromJson<Data>(File.ReadAllText(_path));

            foreach (var item in data.items)
            {
                this.Items.Add(item);
            }

            return true;
        }

        public override bool Save()
        {
            Data data = new Data(Items);
            File.WriteAllText(_path, JsonUtility.ToJson(data));
            return true;
        }

        public bool CreateDefaultData()
        {
            Data data = new Data(Enumerable.Repeat(ItemPair.empty, Global.INVENTORY_SLOT_NUMBER_DEFAULT));
            File.WriteAllText(_path, JsonUtility.ToJson(data));
            return true;
        }
    }
}
