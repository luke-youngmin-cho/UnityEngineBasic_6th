using RPG.Datum;
using RPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Controllers
{
    public class InventorySlotController : Controller
    {
        private InventorySlot _selected;
        [SerializeField] private Transform _selectedIconImage;

        public void Select(InventorySlot slot)
        {
            _selected = slot;
            _selectedIconImage.GetComponent<Image>().sprite = ItemInfoAssets.instance[slot.itemPair.id].icon;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
        }

        private void OnGUI()
        {
            Event current = Event.current;

            _selectedIconImage.position = current.mousePosition;
        }
    }
}
