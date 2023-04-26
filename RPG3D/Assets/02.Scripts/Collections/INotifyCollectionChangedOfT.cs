using System;

namespace RPG.Collections
{
    public interface INotifyCollectionChanged<T>
    {
        // <index of item, item>
        event Action<int, T> itemAdded;
        event Action<int, T> itemRemoved;
        event Action<int, T> itemChanged;
        event Action collectionChanged;
    }
}