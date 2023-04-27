using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Tools;

namespace RPG.Datum
{
    public class ItemInfoAssets : SingletonMonoBase<ItemInfoAssets>
    {
        public ItemInfo this[int id] => _itemsDictionary[id];
        public ItemInfo this[ItemID id] => _itemsDictionary[id.value];

        [SerializeField] private ItemInfo[] _items;
        private Dictionary<int, ItemInfo> _itemsDictionary;

        protected override void Init()
        {
            base.Init();
            _itemsDictionary = new Dictionary<int, ItemInfo>();
            for (int i = 0; i < _items.Length; i++)
            {
                _itemsDictionary.Add(_items[i].id.value, _items[i]);
            }
        }
    }
}