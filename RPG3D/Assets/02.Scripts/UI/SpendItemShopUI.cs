using RPG.DependencySources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class SpendItemShopUI : UIMonoBehaviour
    {
        [SerializeField] private SpendItemShopPurchaseSlot _purchaseSlotPrefab;
        [SerializeField] private SpendItemShopSellSlot _sellSlotPrefab;
        [SerializeField] private Transform _purchaseContent;
        [SerializeField] private Transform _sellContent;
        [SerializeField] private Button _close;
        private SpendItemShopPresenter _presenter;
        private List<SpendItemShopPurchaseSlot> _purchaseSlots;
        private List<SpendItemShopSellSlot> _sellSlots;

        public SpendItemShopPresenter GetPresenter() => _presenter;

        protected override void Init()
        {
            base.Init();
            _presenter = new SpendItemShopPresenter();
            _purchaseSlots = new List<SpendItemShopPurchaseSlot>();
            for (int i = 0; i < _presenter.spendItemShopSource.Count; i++)
            {
                SpendItemShopPurchaseSlot slot = Instantiate(_purchaseSlotPrefab, _purchaseContent);
                slot.index = i;
                _purchaseSlots.Add(slot);
                slot.itemID = _presenter.spendItemShopSource[i];
            }

            _sellSlots = new List<SpendItemShopSellSlot>();
            for (int i = 0; i < _presenter.inventorySource.Count; i++)
            {
                SpendItemShopSellSlot slot = Instantiate(_sellSlotPrefab, _sellContent);
                slot.index = i;
                _sellSlots.Add(slot);
                slot.itemPair = _presenter.inventorySource[i];
            }
            _presenter.inventorySource.itemChanged += (index, itemPair) =>
            {
                _sellSlots[index].itemPair = itemPair;
            };

            _close.onClick.AddListener(() => Hide());
        }
    }
}