using RPG.DataStructures;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.DataModels
{
    public class SpendItemShopDataModel : CollectionDataModelBase<ItemPricePair>
    {
        private string _path;

        [Serializable]
        public class Data
        {
            public List<ItemPricePair> items;

            public Data(IEnumerable<ItemPricePair> copy)
            {
                items = new List<ItemPricePair>(copy);
            }
        }

        public SpendItemShopDataModel()
        {
            _path = Application.persistentDataPath + "/SpendItemShopData.json";
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
            throw new System.NotImplementedException();
        }

        private void CreateDefaultData()
        {
            Data data = new Data(new List<ItemPricePair>()
            {
                new ItemPricePair(122, new Gold(0,0,0,1200)),
            });
            File.WriteAllText(_path, JsonUtility.ToJson(data));
        }
    }
}