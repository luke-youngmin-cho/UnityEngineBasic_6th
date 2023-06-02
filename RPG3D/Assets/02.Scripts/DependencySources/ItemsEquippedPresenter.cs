using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Collections;
using RPG.Controllers;
using RPG.DataModels;
using RPG.DataStructures;
using RPG.Datum;
using RPG.GameElements;
using RPG.GameElements.Items;

namespace RPG.DependencySources
{
    public class ItemsEquippedPresenter
    {
        public ItemsEquippedSource itemsEquippedSource;
        public InventorySource inventorySource;
        private ItemsEquippedDataModel _itemsEquippedDataModel;
        private InventoryDataModel _inventoryDataModel;

        public UnequipCommand unequipCommand;

        public ItemsEquippedPresenter()
        {
            _itemsEquippedDataModel = DataModelManager.instance.Get<ItemsEquippedDataModel>();
            _inventoryDataModel = DataModelManager.instance.Get<InventoryDataModel>();

            itemsEquippedSource = new ItemsEquippedSource(_itemsEquippedDataModel);
            _itemsEquippedDataModel.itemChanged += (slotIndex, itemID) => itemsEquippedSource.Set(slotIndex, itemID);

            inventorySource = new InventorySource(_inventoryDataModel);
            _inventoryDataModel.itemChanged += (slotIndex, itemPair) => inventorySource.Set(slotIndex, itemPair);

            unequipCommand = new UnequipCommand(this);
        }

        public class ItemsEquippedSource : ObservableCollection<int>
        {
            public ItemsEquippedSource(IEnumerable<int> copy)
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

        public class UnequipCommand
        {
            private ItemsEquippedPresenter _presenter;

            public UnequipCommand(ItemsEquippedPresenter presenter)
            {
                _presenter = presenter;
            }

            public bool TryExecute(BodyPartType bodyPartType)
            {
                if (ControllerManager.instance.TryGet(out PlayerController playerController) == false)
                    return false;
                    
                ItemPair item = new ItemPair(playerController.GetEquipment(bodyPartType).id, 1);
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

                if (result)
                {
                    if (playerController.TryUnequip(bodyPartType))
                    {
                        _presenter._itemsEquippedDataModel.RemoveAt((int)bodyPartType);
                        tmpHandler?.Invoke();
                    }
                }

                return result;
            }
        }
    }
}
