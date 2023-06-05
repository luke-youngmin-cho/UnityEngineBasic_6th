using RPG.Datum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RPG.Controllers;
using RPG.DataStructures;
using UnityEngine.Rendering.UI;

namespace RPG.UI
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        public int index;
        public ItemPair itemPair
        {
            get
            {
                return _itemPair;
            }
            set
            {
                if (value.id > 0 && value.num > 0)
                {
                    _icon.sprite = ItemInfoAssets.instance[value.id].icon;
                    _icon.color = Color.white;
                    _num.text = value.num.ToString();
                }
                else
                {
                    _icon.sprite = null;
                    _icon.color = Color.clear;
                    _num.text = string.Empty;
                }
                _itemPair = value;
            }
        }
        private ItemPair _itemPair;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _num;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_itemPair == ItemPair.empty)
                return;

            if (ControllerManager.instance.IsAuthorized<InventorySlotController>())
                return;

            // 왼쪽 클릭시
            if (eventData.button == PointerEventData.InputButton.Left)
                ControllerManager.instance.Get<InventorySlotController>().Select(this);
            // 오른쪽 클릭시
            else if (eventData.button == PointerEventData.InputButton.Right)
                ItemInfoAssets.instance[_itemPair.id].Use();
            
        }
    }
}
