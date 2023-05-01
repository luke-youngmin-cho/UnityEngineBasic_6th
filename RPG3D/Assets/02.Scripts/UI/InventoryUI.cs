using RPG.DependencySources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class InventoryUI : UIMonoBehaviour
    {
        [SerializeField] private InventorySlot _slotPrefab;
        [SerializeField] private Transform _content;
        public InventoryPresenter _presenter;
        private List<InventorySlot> _slots;
        protected override void Init()
        {
            base.Init();
            _presenter = new InventoryPresenter();
            _slots = new List<InventorySlot>();
            for (int i = 0; i < _presenter.inventorySource.Count; i++)
            {
                InventorySlot slot = Instantiate(_slotPrefab, _content);
                _slots.Add(slot);
                slot.index = i;
                slot.itemPair = _presenter.inventorySource[i];
            }
            _presenter.inventorySource.itemChanged += (slotIndex, itemPair) => _slots[slotIndex].itemPair = itemPair;
        }
    }
}