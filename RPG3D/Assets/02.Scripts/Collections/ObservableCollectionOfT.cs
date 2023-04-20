using System;
using System.Collections.ObjectModel;

namespace RPG.Collections
{
    public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged<T>
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
            itemAdded?.Invoke(item);
            collectionChanged?.Invoke();
        }

        protected override void RemoveItem(int index)
        {
            T expected = Items[index];
            base.RemoveItem(index);
            itemRemoved?.Invoke(expected);
            collectionChanged?.Invoke();
        }

        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            itemChanged?.Invoke(item);
            collectionChanged?.Invoke();
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            collectionChanged?.Invoke();
        }
    }
}
