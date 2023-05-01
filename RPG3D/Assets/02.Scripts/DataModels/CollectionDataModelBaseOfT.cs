using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using RPG.Collections;

namespace RPG.DataModels
{
    [Serializable]
    public abstract class CollectionDataModelBase<T> : Collection<T>, INotifyCollectionChanged<T>
    {
        public event Action<int, T> itemAdded;
        public event Action<int, T> itemRemoved;
        public event Action<int, T> itemChanged;
        public event Action collectionChanged;

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (match.Invoke(Items[i]))
                    return Items[i];
            }
            return default(T);
        }
        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (match.Invoke(Items[i]))
                    return i;
            }
            return -1;
        }

        public void Set(int index, T item)
        {
            SetItem(index, item);
        }

        public void Swap(int index1, int index2)
        {
            T tmp1 = Items[index1];
            T tmp2 = Items[index2];
            base.SetItem(index1, tmp2);
            base.SetItem(index2, tmp1);
            if (Save())
            {
                itemChanged?.Invoke(index1, tmp2);
                itemChanged?.Invoke(index2, tmp1);
                collectionChanged?.Invoke();
            }
            else
            {
                throw new Exception($"[CollectionDataModelBase] : Failed To Swap index {index1} & index {index2}");
            }
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            if (Save())
            {
                itemAdded?.Invoke(index, item);
                collectionChanged?.Invoke();
            }
            else
            {
                throw new Exception($"[CollectionDataModelBase<{typeof(T)}>] : Failed to save data.");
            }
        }

        protected override void RemoveItem(int index)
        {
            T expected = Items[index];
            base.RemoveItem(index);
            if (Save())
            {
                itemRemoved?.Invoke(index, expected);
                collectionChanged?.Invoke();
            }
            else
            {
                throw new Exception($"[CollectionDataModelBase<{typeof(T)}>] : Failed to save data.");
            }
        }


        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            if (Save())
            {
                itemChanged?.Invoke(index, item);
                collectionChanged?.Invoke();
            }
            else
            {
                throw new Exception($"[CollectionDataModelBase<{typeof(T)}>] : Failed to save data.");
            }
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            collectionChanged?.Invoke();
        }

        protected virtual void OnItemAdded(int index, T item)
        {
            itemAdded?.Invoke(index, item);
        }

        protected virtual void OnItemChanged(int index, T item)
        {
            itemChanged?.Invoke(index,item);
        }

        protected virtual void OnItemRemoved(int index, T item)
        {
            itemRemoved?.Invoke(index, item);
        }

        protected virtual void OnCollectionChanged()
        {
            collectionChanged?.Invoke();
        }
        public abstract bool Save();
        public abstract bool Load();
    }
}