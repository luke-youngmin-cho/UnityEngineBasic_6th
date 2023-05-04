using RPG.DataStructures;
using System.IO;
using UnityEngine;

namespace RPG.DataModels
{
    public class GoldDataModel : DataModelBase<Gold>
    {
        public GoldDataModel()
        {
            path = Application.persistentDataPath + "/Gold.json";
            Load();
        }        
    }
}