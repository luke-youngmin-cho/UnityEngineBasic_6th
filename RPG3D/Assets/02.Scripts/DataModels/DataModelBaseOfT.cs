using RPG.DataStructures;
using System;
using System.IO;
using UnityEngine;

namespace RPG.DataModels
{
    public abstract class DataModelBase<T>
    {
        public T data { get; protected set; }
        public event Action<T> dataChanged;
        protected string path;

        public virtual void SetData(T value)
        {
            data = value;
            if (Save())
                dataChanged?.Invoke(value);
        }

        public virtual bool Load()
        {
            if (File.Exists(path) == false)
            {
                CreateDefaultData();
            }

            data = JsonUtility.FromJson<T>(File.ReadAllText(path));

            return true;
        }

        public virtual bool Save()
        {
            File.WriteAllText(path, JsonUtility.ToJson(data));
            return true;
        }

        protected virtual void CreateDefaultData()
        {
            data = default(T);
            File.WriteAllText(path, JsonUtility.ToJson(data)); // <- 추가해주삼
        }
    }
}
