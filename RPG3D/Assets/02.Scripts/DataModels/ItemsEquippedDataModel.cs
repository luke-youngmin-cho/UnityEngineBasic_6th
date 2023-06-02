using RPG.DataStructures;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.GameElements.Items;

namespace RPG.DataModels
{
    public class ItemsEquippedDataModel : CollectionDataModelBase<int>
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


        public ItemsEquippedDataModel()
        {
            _path = Application.persistentDataPath + "/ItemsEquippedData.json";
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
            Data data = new Data(Items);
            File.WriteAllText(_path, JsonUtility.ToJson(data));
            return true;
        }

        public bool CreateDefaultData()
        {
            List<int> tmp = new List<int>();
            foreach (var item in Enum.GetValues(typeof(BodyPartType)))
            {
                tmp.Add(-1); // empty
            }
            Data data = new Data(tmp);
            File.WriteAllText(_path, JsonUtility.ToJson(data));
            return true;
        }
    }
}