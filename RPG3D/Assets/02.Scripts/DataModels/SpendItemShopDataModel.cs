using RPG.DataStructures;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.DataModels
{
    public class SpendItemShopDataModel : CollectionDataModelBase<int>
    {
        private string _path;

        [Serializable]
        public class Data
        {
            public List<int> items;

            public Data(IEnumerable<int> copy)
            {
                items = new List<int>(copy);
            }
        }

        public SpendItemShopDataModel()
        {
            _path = Application.persistentDataPath + "/SpendItemShopData.json";
            Load();
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
            Data data = new Data(new List<int>()
            {
                122,
            });
            File.WriteAllText(_path, JsonUtility.ToJson(data));
        }
    }
}