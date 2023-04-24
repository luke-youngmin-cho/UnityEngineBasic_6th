using RPG.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.DataModels
{
    public class DataModelManager : SingletonBase<DataModelManager>
    {
        public List<object> models;

        public T Get<T>()
        {
            return (T)models.Find(x => x is T);
        }

        protected override void Init()
        {
            base.Init();

            models = new List<object>()
            {
                new InventoryDataModel(),
            };
        }
    }
}
