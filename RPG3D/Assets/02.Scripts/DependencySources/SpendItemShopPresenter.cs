using RPG.Collections;
using RPG.DataModels;
using RPG.DataStructures;
using System;
using System.Collections.Generic;

namespace RPG.DependencySources
{
    public class SpendItemShopPresenter
    {
        public SpendItemShopSource spendItemShopSource;
        public InventorySource inventorySource;
        public GoldSource goldSource;

        public SpendItemShopPresenter()
        {
            SpendItemShopDataModel spendItemShopDataModel = DataModelManager.instance.Get<SpendItemShopDataModel>();
            spendItemShopSource = new SpendItemShopSource(spendItemShopDataModel);
            spendItemShopDataModel.itemChanged += (slotID, itemPricePair) => spendItemShopSource.Set(slotID, itemPricePair);

            InventoryDataModel inventoryDataModel = DataModelManager.instance.Get<InventoryDataModel>();
            inventorySource = new InventorySource(inventoryDataModel);
            inventoryDataModel.itemChanged += (slotID, itemPair) => inventorySource.Set(slotID, itemPair);

            GoldDataModel goldDataModel = DataModelManager.instance.Get<GoldDataModel>();
            goldSource = new GoldSource(goldDataModel.data);
            goldDataModel.dataChanged += (value) => goldSource.SetData(value);
        }

        public class SpendItemShopSource : ObservableCollection<ItemPricePair>
        {
            public SpendItemShopSource(IEnumerable<ItemPricePair> copy)
            {
                foreach (var item in copy)
                {
                    Items.Add(item);
                }
            }
        }

        public class InventorySource : ObservableCollection<ItemPair>
        {
            public InventorySource(IEnumerable<ItemPair> copy)
            {
                foreach (var item in copy)
                {
                    Items.Add(item);
                }
            }
        }

        public class GoldSource
        {
            public Gold data => _data;
            private Gold _data;
            public event Action<Gold> dataChanged;

            public GoldSource(Gold copy)
            {
                _data = copy;
            }

            public void SetData(Gold value)
            {
                _data = value;
                dataChanged?.Invoke(value);
            }
        }

        public class PurchaseCommand
        {
            private SpendItemShopPresenter _presenter;

            public PurchaseCommand(SpendItemShopPresenter presenter) => _presenter = presenter;

            public bool CanExecute(ItemPricePair pricePair, int num)
            {
                Gold expected = pricePair.price * num;
                return _presenter.goldSource.data >= expected;
            }

            public void Execute(ItemPricePair pricePair, int num)
            {

            }

            public void TryExecute(ItemPricePair pricePair, int num)
            {

            }
        }
    }
}
