using System;

namespace RPG.Collections
{
    public interface INotifyCollectionChanged<T>
    {
        event Action<T> itemAdded;
        event Action<T> itemRemoved;
        event Action<T> itemChanged;
        event Action collectionChanged;
    }
}