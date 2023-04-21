using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using RPG.Collections;

namespace RPG.DataModels
{
    public abstract class CollectionDataModelBase<T> : Collection<T>, INotifyCollectionChanged<T>
    {
        public event Action<T> itemAdded;
        public event Action<T> itemRemoved;
        public event Action<T> itemChanged;
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

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            if (Save())
            {
                itemAdded?.Invoke(item);
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
                itemRemoved?.Invoke(expected);
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
                itemChanged?.Invoke(item);
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

        protected virtual void OnItemAdded(T item)
        {
            itemAdded?.Invoke(item);
        }

        protected virtual void OnItemChanged(T item)
        {
            itemChanged?.Invoke(item);
        }

        protected virtual void OnItemRemoved(T item)
        {
            itemRemoved?.Invoke(item);
        }

        protected virtual void OnCollectionChanged()
        {
            collectionChanged?.Invoke();
        }
        public abstract bool Save();
        public abstract bool Load();
    }
}