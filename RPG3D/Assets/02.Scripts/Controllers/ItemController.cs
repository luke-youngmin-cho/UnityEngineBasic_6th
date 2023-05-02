using RPG.DataModels;
using RPG.Datum;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

namespace RPG.Controllers
{
    [RequireComponent(typeof(BoxCollider), typeof(MeshFilter), typeof(MeshRenderer))]
    public class ItemController : Controller
    {
        public ItemPair itemPair;

        public static ItemController Create(ItemPair itemPair, Vector3 position)
        {            
            ItemInfo itemInfo = ItemInfoAssets.instance[itemPair.id];
            GameObject go = new GameObject(itemInfo.name);
            ItemController controller = go.AddComponent<ItemController>();
            controller.itemPair = itemPair;
            go.GetComponent<BoxCollider>().size = Vector3.one * 2.0f;
            go.GetComponent<MeshFilter>().mesh = itemInfo.mesh;
            go.GetComponent<MeshRenderer>().material = itemInfo.material;
            go.transform.localScale = Vector3.one * 0.5f;
            go.transform.position = position;
            return controller;
        }


        public void PickUp()
        {
            bool result = false;
            int remains = itemPair.num;
            ItemPair current;
            Action tmpHandler = null;
            InventoryDataModel dataModel = DataModelManager.instance.Get<InventoryDataModel>();
            for (int i = 0; i < dataModel.Count; i++)
            {
                current = dataModel[i];
                if ((current.id == itemPair.id && current.num < ItemInfoAssets.instance[itemPair.id].numMax) ||
                    current == ItemPair.empty)
                {
                    result = true;
                    int expected = remains - (ItemInfoAssets.instance[itemPair.id].numMax - current.num);
                    int tmpIdx = i;
                    ItemPair tmpPair;
                    if (expected > 0)
                    {
                        tmpPair = new ItemPair(itemPair.id, ItemInfoAssets.instance[itemPair.id].numMax);
                        tmpHandler += () => dataModel.Set(tmpIdx, tmpPair);
                        remains = expected;
                    }
                    else
                    {
                        tmpPair = new ItemPair(itemPair.id, current.num + remains);
                        tmpHandler += () => dataModel.Set(tmpIdx, tmpPair);
                        remains = 0;
                        break;
                    }
                }
            }

            if (result)
                tmpHandler?.Invoke();

            if (remains > 0)
            {
                itemPair = new ItemPair(itemPair.id, remains);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}