using RPG.DataModels;
using RPG.Datum;
using RPG.InputSystems;
using RPG.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Controllers
{
    public class InventorySlotController : Controller
    {
        public override bool controllable
        {
            get
            {
                return _controllable;
            }
            set
            {
                gameObject.SetActive(value);
                _controllable = value;
            }
        }
        private bool _controllable;
        private InventorySlot _selected;
        [SerializeField] private Transform _selectedIconImage;
        [SerializeField] private CustomStandaloneInputModule _inputModule;
        private Canvas _canvas;
        private InventoryDataModel _inventoryDataModel;

        public void Select(InventorySlot slot)
        {
            _selected = slot;
            _selectedIconImage.GetComponent<Image>().sprite = ItemInfoAssets.instance[slot.itemPair.id].icon;
            _selectedIconImage.gameObject.SetActive(true);
            ControllerManager.instance.Authorize<InventorySlotController>();
        }

        public void Deselect(float delay)
        {
            _selected = null;
            _selectedIconImage.gameObject.SetActive(false);
            Invoke("Dismiss", delay);
        }

        private void Dismiss()
        {
            ControllerManager.instance.Dismiss<InventorySlotController>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // UI 위를 누를시
                if (_inputModule.TryGetHovered<GraphicRaycaster>(out List<GameObject> hovered))
                {
                    // todo -> 어떤 UI 인지 컴포넌트 받아와서 타입에따라 처리
                    foreach (var sub in hovered)
                    {
                        if (sub == _selected.gameObject)
                            continue;

                        if (sub.TryGetComponent(out InventorySlot slot))
                        {
                            _inventoryDataModel.Swap(_selected.index, slot.index);
                            break;
                        }
                    }
                }
                // World 위를 누를시
                else
                {
                    // todo -> 아이템 버리기
                }

                Deselect(0.1f);
            }
        }



        protected override void Awake()
        {
            base.Awake();
            _canvas = transform.GetComponentInParent<Canvas>();
            _inventoryDataModel = DataModelManager.instance.Get<InventoryDataModel>();
        }

        private void OnGUI()
        {
            Event current = Event.current;
            _selectedIconImage.position = new Vector3(current.mousePosition.x, Screen.height - current.mousePosition.y);
        }
    }
}
