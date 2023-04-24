using RPG.Collections;
using RPG.DataModels;
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
                int slotIndex = _presenter.inventorySource.FindIndex(x => x.id == item.id);

                if (slotIndex < 0)
                    slotIndex = _presenter.inventorySource.FindIndex(x => x == ItemPair.empty);

                return slotIndex >= 0;
            }

            public void Execute(ItemPair item)
            {
                _presenter._inventoryDataModel.Add(item);
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
