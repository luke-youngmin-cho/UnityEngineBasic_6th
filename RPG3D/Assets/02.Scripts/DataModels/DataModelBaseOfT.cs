using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.DataModels
{
    public abstract class DataModelBase<T>
    {
        public T data;
        public event Action<T> dataChanged;
        public abstract bool Save();
        public abstract bool Load();
    }
}
