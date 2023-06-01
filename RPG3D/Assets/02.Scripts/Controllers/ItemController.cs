using RPG.DataModels;
using RPG.Datum;
using System;
using UnityEngine;
using RPG.DataStructures;
using System.Collections;
using RPG.GameSystems;

namespace RPG.Controllers
{
    [RequireComponent(typeof(BoxCollider), typeof(MeshFilter), typeof(MeshRenderer))]
    public class ItemController : Controller
    {
        public ItemPair itemPair;
        private bool _hasBeenPicked;
        public static ItemController Create(ItemPair itemPair, Vector3 position)
        {            
            ItemInfo itemInfo = ItemInfoAssets.instance[itemPair.id];
            GameObject go = new GameObject(itemInfo.name);
            go.layer = LayerMask.NameToLayer("ItemController");
            ItemController controller = go.AddComponent<ItemController>();
            controller.itemPair = itemPair;
            go.GetComponent<BoxCollider>().size = Vector3.one * 2.0f;
            go.GetComponent<BoxCollider>().isTrigger = true;
            go.GetComponent<MeshFilter>().mesh = itemInfo.mesh;
            go.GetComponent<MeshRenderer>().material = itemInfo.material;
            go.transform.localScale = Vector3.one * 0.5f;
            go.transform.position = position;
            return controller;
        }


        public void PickUp(Transform owner)
        {
            if (_hasBeenPicked)
                return;

            GetComponent<BoxCollider>().enabled = false;
            _hasBeenPicked = true;

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
                StartCoroutine(Follow(owner));
            }
        }

        private IEnumerator Follow(Transform owner)
        {
            while (Vector3.Distance(transform.position, owner.position + Vector3.up) > 0.2f)
            {
                transform.position = Vector3.Lerp(transform.position, owner.position + Vector3.up, 0.05f);
                yield return null;
            }

            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}