using RPG.Collections;
using RPG.DataModels;
using RPG.Datum;
using System.Collections.Generic;
using System.Windows.Input;

namespace RPG.DependencySources
{
    public class InventoryPresenter
    {
        public InventorySource inventorySource;
        public AddCommand addCommand;
        private InventoryDataModel _inventoryDataModel;
        public InventoryPresenter()
        {
            _inventoryDataModel = DataModelManager.instance.Get<InventoryDataModel>();
            inventorySource = new InventorySource(_inventoryDataModel);
            addCommand = new AddCommand(this);
        }

        #region Inventory Source
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
        #endregion

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
                            _presenter.inventorySource.Set(i, new ItemPair(current.id, ItemInfoAssets.instance[item.id].numMax));
                            remains = expected;
                            i++;
                        }
                        else
                        {
                            _presenter.inventorySource.Set(i, new ItemPair(current.id, current.num + remains));
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
                for (int i = 0; i < _presenter.inventorySource.Count; i++)
                {
                    current = _presenter.inventorySource[i];
                    if ((current.id == item.id && current.num < ItemInfoAssets.instance[item.id].numMax) ||
                        current == ItemPair.empty)
                    {
                        result = true;
                        int expected = remains - (ItemInfoAssets.instance[item.id].numMax - current.num);
                        if (expected > 0)
                        {
                            _presenter.inventorySource.Set(i, new ItemPair(current.id, ItemInfoAssets.instance[item.id].numMax));
                            remains = expected;
                            i++;
                        }
                        else
                        {
                            _presenter.inventorySource.Set(i, new ItemPair(current.id, current.num + remains));
                            remains = 0;
                            return result;
                        }
                    }
                }
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
                return _presenter
                        .inventorySource
                        .Find(x => x.id == item.id && x.num >= item.num)
                        .id > 0;
            }

            public void Execute(ItemPair item)
            {
                _presenter._inventoryDataModel.Remove(item);
            }

            public bool TryExecute(ItemPair item)
            {
                if (CanExecute(item))
                {
                    Execute(item);
                    return true;
                }

                return false;
            }
        }
    }
}
