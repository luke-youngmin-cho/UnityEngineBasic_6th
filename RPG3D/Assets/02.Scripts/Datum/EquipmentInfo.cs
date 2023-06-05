using RPG.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.DataStructures;
using RPG.GameElements.Items;

namespace RPG.Datum
{
    [CreateAssetMenu(fileName = "new EquipmentInfo", menuName = "RPG/Create a new EquipmentInfo")]
    public class EquipmentInfo : ItemInfo
    {
        public BodyPartType bodyPartType;

        public override void Use()
        {
            InventoryDataModel inventoryDataModel = DataModelManager.instance.Get<InventoryDataModel>();
            ItemsEquippedDataModel itemsEquippedDataModel = DataModelManager.instance.Get<ItemsEquippedDataModel>();

            int equippedItemID = itemsEquippedDataModel[(int)bodyPartType];
            inventoryDataModel.Remove(new ItemPair(id.value, 1));
            itemsEquippedDataModel.Set((int)bodyPartType, id.value);
            if (equippedItemID > 0)
                inventoryDataModel.Add(new ItemPair(equippedItemID, 1));
        }
    }
}