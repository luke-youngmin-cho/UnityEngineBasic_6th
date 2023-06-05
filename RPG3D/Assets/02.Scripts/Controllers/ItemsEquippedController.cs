using RPG.DataStructures;
using RPG.Datum;
using RPG.GameElements.Items;
using RPG.GameElements;
using RPG.InputSystems;
using RPG.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RPG.DataModels;

namespace RPG.Controllers
{
    public class ItemsEquippedController : Controller
    {
        [SerializeField] private CustomStandaloneInputModule _inputModule;
        [SerializeField] private Transform _imageFollowingCursor;
        private ItemsEquippedSlot _selected;

        private void Update()
        {
            if (controllable == false) 
                return;

            if (_selected == null)
            {
                // 슬롯에 왼쪽 마우스 눌렀을때 장착한 아이템 선택하기
                if (Input.GetMouseButtonDown(0))
                {
                    List<GameObject> hovered;
                    if (_inputModule.TryGetHovered<GraphicRaycaster>(out hovered, StandaloneInputModule.kMouseLeftId))
                    {
                        foreach (var sub in hovered)
                        {
                            if (sub.TryGetComponent(out ItemsEquippedSlot slot))
                            {
                                if (slot.itemID > 0)
                                {
                                    _selected = slot;
                                    _imageFollowingCursor.gameObject.SetActive(true);
                                    _imageFollowingCursor.GetComponent<Image>().sprite = ItemInfoAssets.instance[slot.itemID].icon;
                                }
                            }
                        }
                    }
                    else if (_inputModule.TryGetHovered<GraphicRaycaster>(out hovered, StandaloneInputModule.kMouseRightId))
                    {
                        foreach (var sub in hovered)
                        {
                            if (sub.TryGetComponent(out ItemsEquippedSlot slot))
                            {
                                if (slot.itemID > 0)
                                {
                                    if (TryUnequip(slot.bodyPartType))
                                    {
                                        Cancel();
                                    }
                                    else
                                    {
                                        throw new Exception($"[ItemsEquippedController] : Failed to unequip item {slot.itemID}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                _imageFollowingCursor.position = Input.mousePosition;

                if (Input.GetMouseButtonDown(1))
                {
                    Cancel();
                }
            }
        }

        private void Cancel()
        {
            _selected = null;
            _imageFollowingCursor.gameObject.SetActive(false);
        }

        public bool TryUnequip(BodyPartType bodyPartType)
        {
            if (ControllerManager.instance.TryGet(out PlayerController playerController) == false)
                return false;

            InventoryDataModel inventoryDataModel = DataModelManager.instance.Get<InventoryDataModel>();
            ItemsEquippedDataModel itemsEquippedDataModel = DataModelManager.instance.Get<ItemsEquippedDataModel>();
            ItemPair item = new ItemPair(playerController.GetEquipment(bodyPartType).id, 1);
            bool result = false;
            int remains = item.num;
            ItemPair current;
            Action tmpHandler = null;

            for (int i = 0; i < inventoryDataModel.Count; i++)
            {
                current = inventoryDataModel[i];
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
                        tmpHandler += () => inventoryDataModel.Set(tmpIdx, tmpPair);
                        remains = expected;
                    }
                    else
                    {
                        tmpPair = new ItemPair(item.id, current.num + remains);
                        tmpHandler += () => inventoryDataModel.Set(tmpIdx, tmpPair);
                        remains = 0;
                        break;
                    }
                }
            }

            if (result)
            {
                if (playerController.TryUnequip(bodyPartType))
                {
                    itemsEquippedDataModel.RemoveAt((int)bodyPartType);
                    tmpHandler?.Invoke();
                }
            }

            return result;
        }
    }
}
