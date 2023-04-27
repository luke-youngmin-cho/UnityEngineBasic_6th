using RPG.Collections;
using RPG.DataModels;
using RPG.Datum;
using System.Collections.Generic;
using System;

namespace RPG.DependencySources
{
    public class InventoryPresenter
    {
        public InventorySource inventorySource;
        public AddCommand addCommand;
        public RemoveCommand removeCommand;
        private InventoryDataModel _inventoryDataModel;

        public InventoryPresenter()
        {
            _inventoryDataModel = DataModelManager.instance.Get<InventoryDataModel>();
            inventorySource = new InventorySource(_inventoryDataModel);

            _inventoryDataModel.itemChanged += (slotIndex, itemPair) => inventorySource.Set(slotIndex, itemPair);

            addCommand = new AddCommand(this);
            removeCommand = new RemoveCommand(this);
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

        public class AddCommand
        {
            private InventoryPresenter _presenter;
            public AddCommand(InventoryPresenter presenter)
            {
                _presenter = presenter;
            }

            public bool CanExecute(ItemPair item)
            {
                int remains = item.num;
                ItemPair current;
                for (int i = 0; i < _presenter.inventorySource.Count; i++)
                {
                    current = _presenter.inventorySource[i];
                    if ((current.id == item.id && current.num < ItemInfoAssets.instance[item.id].numMax) ||
                        current == ItemPair.empty)
                    {
                        remains = remains - (ItemInfoAssets.instance[item.id].numMax - current.num);
                        if (remains > 0)
                        {
                            i++;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            public bool CanExecute(ItemPair item, out int remains)
            {
                bool result = false;
                remains = item.num;
                ItemPair current;
                for (int i = 0; i < _presenter.inventorySource.Count; i++)
                {
                    current = _presenter.inventorySource[i];
                    if ((current.id == item.id && current.num < ItemInfoAssets.instance[item.id].numMax) ||
                        current == ItemPair.empty)
                    {
                        result = true;
                        remains = remains - (ItemInfoAssets.instance[item.id].numMax - current.num);
                        if (remains > 0)
                        {
                            i++;
                        }
                        else
                        {
                            remains = 0;
                            return result;
                        }
                    }
                }

                return result;
            }
            public void Execute(ItemPair item, out int remains)
            {
                remains = item.num;
                ItemPair current;
                for (int i = 0; i < _presenter.inventorySource.Count; i++)
                {
                    current = _presenter.inventorySource[i];
                    if ((current.id == item.id && current.num < ItemInfoAssets.instance[item.id].numMax) ||
                        current == ItemPair.empty)
                    {
                        int expected = remains - (ItemInfoAssets.instance[item.id].numMax - current.num);
                        if (expected > 0)
                        {
                            _presenter.inventorySource.Set(i, new ItemPair(item.id, ItemInfoAssets.instance[item.id].numMax));
                            remains = expected;
                            i++;
                        }
                        else
                        {
                            _presenter.inventorySource.Set(i, new ItemPair(item.id, current.num + remains));
                            remains = 0;
                            return;
                        }
                    }
                }
            }

            public bool TryExecute(ItemPair item, out int remains)
            {
                bool result = false;
                remains = item.num;
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
                            tmpHandler += () => _presenter.inventorySource.Set(tmpIdx, tmpPair);
                            remains = expected;
                            i++;
                        }
                        else
                        {
                            tmpPair = new ItemPair(item.id, current.num + remains);
                            tmpHandler += () => _presenter.inventorySource.Set(tmpIdx, tmpPair);
                            remains = 0;
                            break;
                        }
                    }
                }

                if (result)
                    tmpHandler?.Invoke();


                return result;
            }
        }

        public class RemoveCommand
        {
            private InventoryPresenter _presenter;
            public RemoveCommand(InventoryPresenter presenter)
            {
                _presenter = presenter;
            }

            public bool CanExecute(ItemPair item)
            {
                int sum = 0;
                for (int i = 0; i < _presenter.inventorySource.Count; i++)
                {
                    if (_presenter.inventorySource[i].id == item.id)
                    {
                        sum += _presenter.inventorySource[i].num;
                        if (sum >= item.num)
                            return true;
                    }
                }

                return false;
            }

            public void Execute(ItemPair item)
            {
                int remains = item.num;
                for (int i = 0; i < _presenter.inventorySource.Count; i++)
                {
                    if (_presenter.inventorySource[i].id == item.id)
                    {
                        int expected = remains - _presenter.inventorySource[i].num;
                        if (expected > 0)
                        {
                            _presenter.inventorySource.Set(i, ItemPair.empty);
                            remains = expected;
                        }
                        else
                        {
                            _presenter.inventorySource.Set(i, new ItemPair(item.id, -expected));
                            remains = 0; 
                        }                        
                    }
                }
            }

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
                            tmpHandler += () => _presenter.inventorySource.Set(tmpIdx, ItemPair.empty);
                            remains = expected;
                        }
                        else
                        {
                            tmpHandler += () => _presenter.inventorySource.Set(tmpIdx, new ItemPair(item.id, -expected));
                            remains = 0;
                            result = true;
                            break;
                        }
                    }
                }

                if (result)
                    tmpHandler?.Invoke();

                return result;
            }
        }
    }
}
