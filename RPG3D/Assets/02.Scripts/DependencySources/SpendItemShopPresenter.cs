using RPG.Collections;
using RPG.DataModels;
using RPG.DataStructures;
using RPG.Datum;
using System;
using System.Collections.Generic;
using static UnityEditor.Progress;

namespace RPG.DependencySources
{
    public class SpendItemShopPresenter
    {
        public SpendItemShopSource spendItemShopSource;
        public InventorySource inventorySource;
        public GoldSource goldSource;

        private SpendItemShopDataModel _spendItemShopDataModel;
        private InventoryDataModel _inventoryDataModel;
        private GoldDataModel _goldDataModel;

        public SpendItemShopPresenter()
        {
            _spendItemShopDataModel = DataModelManager.instance.Get<SpendItemShopDataModel>();
            spendItemShopSource = new SpendItemShopSource(_spendItemShopDataModel);
            _spendItemShopDataModel.itemChanged += (slotID, itemPricePair) => spendItemShopSource.Set(slotID, itemPricePair);

            _inventoryDataModel = DataModelManager.instance.Get<InventoryDataModel>();
            inventorySource = new InventorySource(_inventoryDataModel);
            _inventoryDataModel.itemChanged += (slotID, itemPair) => inventorySource.Set(slotID, itemPair);

            _goldDataModel = DataModelManager.instance.Get<GoldDataModel>();
            goldSource = new GoldSource(_goldDataModel.data);
            _goldDataModel.dataChanged += (value) => goldSource.SetData(value);
        }

        public class SpendItemShopSource : ObservableCollection<int>
        {
            public SpendItemShopSource(IEnumerable<int> copy)
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

            public bool CanExecute(ItemPair item)
            {
                // 돈 충분한지 
                Gold expected = ItemInfoAssets.instance[item.id].purchasePrice * item.num;
                if (_presenter.goldSource.data < expected)
                    return false;

                // 인벤토리 여유 있는지
                int remains = item.num;
                ItemPair current;
                for (int i = 0; i < _presenter.inventorySource.Count; i++)
                {
                    current = _presenter.inventorySource[i];
                    if ((current.id == item.id && current.num < ItemInfoAssets.instance[item.id].numMax) ||
                        current == ItemPair.empty)
                    {
                        remains = remains - (ItemInfoAssets.instance[item.id].numMax - current.num);
                        if (remains <= 0)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            public void Execute(ItemPair item)
            {
                bool result = false;
                int remains = item.num;
                ItemPair current;
                Action tmpHandler = null;

                for (int i = 0; i < _presenter.inventorySource.Count; i++)
                {
                    current = _presenter.inventorySource[i];
                    if ((current.id == item.id && current.num < ItemInfoAssets.instance[item.id].numMax) ||
                        current == ItemPair.empty)
                    {
                        result = true;
                        int expected = remains - (ItemInfoAssets.instance[item.id].numMax - current.num);
                        int tmpIdx = i;
                        ItemPair tmpPair;
                        if (expected > 0)
                        {
                            tmpPair = new ItemPair(item.id, ItemInfoAssets.instance[item.id].numMax);
                            tmpHandler += () => _presenter._inventoryDataModel.Set(tmpIdx, tmpPair);
                            remains = expected;
                        }
                        else
                        {
                            tmpPair = new ItemPair(item.id, current.num + remains);
                            tmpHandler += () => _presenter._inventoryDataModel.Set(tmpIdx, tmpPair);
                            remains = 0;
                            break;
                        }
                    }
                }

                if (result && remains == 0)
                {
                    tmpHandler?.Invoke();
                    _presenter._goldDataModel.SetData(_presenter.goldSource.data - ItemInfoAssets.instance[item.id].purchasePrice * item.num);
                }
                else
                {
                    throw new Exception($"[SpendItemShopPresenter] : Impossible to purchase item. not enough inventory.");
                }
            }

            public bool TryExecute(ItemPair item)
            {
                bool result = false;
                int remains = item.num;
                ItemPair current;
                Action tmpHandler = null;

                for (int i = 0; i < _presenter.inventorySource.Count; i++)
                {
                    current = _presenter.inventorySource[i];
                    if ((current.id == item.id && current.num < ItemInfoAssets.instance[item.id].numMax) ||
                        current == ItemPair.empty)
                    {
                        result = true;
                        int expected = remains - (ItemInfoAssets.instance[item.id].numMax - current.num);
                        int tmpIdx = i;
                        ItemPair tmpPair;
                        if (expected > 0)
                        {
                            tmpPair = new ItemPair(item.id, ItemInfoAssets.instance[item.id].numMax);
                            tmpHandler += () => _presenter._inventoryDataModel.Set(tmpIdx, tmpPair);
                            remains = expected;
                        }
                        else
                        {
                            tmpPair = new ItemPair(item.id, current.num + remains);
                            tmpHandler += () => _presenter._inventoryDataModel.Set(tmpIdx, tmpPair);
                            remains = 0;
                            break;
                        }
                    }
                }

                if (result && remains == 0)
                {
                    tmpHandler?.Invoke();
                    _presenter._goldDataModel.SetData(_presenter.goldSource.data - ItemInfoAssets.instance[item.id].purchasePrice * item.num);
                    return true;
                }

                return false;
            }
        }

        public class SellCommand
        {
            private SpendItemShopPresenter _presenter;
            public SellCommand(SpendItemShopPresenter presenter) => _presenter = presenter;

            public bool TryExecute(ItemPair item)
            {
                bool result = false;
                int remains = item.num;
                Action tmpHandler = null;

                for (int i = 0; i < _presenter.inventorySource.Count; i++)
                {
                    if (_presenter.inventorySource[i].id == item.id)
                    {
                        int expected = remains - _presenter.inventorySource[i].num;
                        int tmpIdx = i;
                        if (expected > 0)
                        {
                            tmpHandler += () => _presenter._inventoryDataModel.Set(tmpIdx, ItemPair.empty);
                            remains = expected;
                        }
                        else
                        {
                            tmpHandler += () => _presenter._inventoryDataModel.Set(tmpIdx, new ItemPair(item.id, -expected));
                            remains = 0;
                            result = true;
                            break;
                        }
                    }
                }

                if (result)
                {
                    tmpHandler?.Invoke();
                    _presenter._goldDataModel.SetData(_presenter._goldDataModel.data + ItemInfoAssets.instance[item.id].sellPrice * item.num);
                    return true;
                }

                return false;
            }
        }
    }
}
