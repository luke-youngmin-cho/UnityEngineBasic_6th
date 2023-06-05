using RPG.Controllers;
using RPG.Datum;
using RPG.DependencySources;
using RPG.GameElements.Items;
using RPG.GameElements.StatSystems;
using RPG.InputSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.UI
{
    public class ItemsEquippedUI : UIMonoBehaviour
    {
        [SerializeField] private ItemsEquippedSlot[] _slots;
        private ItemsEquippedPresenter _presenter;

        public override void Show()
        {
            base.Show();
            ControllerManager.instance.Authorize<ItemsEquippedController>();
        }

        public override void Hide()
        {
            base.Hide();
            ControllerManager.instance.Dismiss<ItemsEquippedController>();
        }

        protected override void Init()
        {
            base.Init();

            _presenter = new ItemsEquippedPresenter();
            _presenter.itemsEquippedSource.itemChanged += (slotID, itemID) =>
            {
                switch ((BodyPartType)slotID)
                {
                    case BodyPartType.None:
                        break;
                    case BodyPartType.Head:
                    case BodyPartType.Top:
                    case BodyPartType.Bottom:
                    case BodyPartType.Feet:
                    case BodyPartType.RightHand:
                    case BodyPartType.LeftHand:
                        {
                            for (int i = 0; i < _slots.Length; i++)
                            {
                                if (_slots[i].bodyPartType == (BodyPartType)slotID)
                                    _slots[i].itemID = itemID;
                            }
                        }
                        break;
                    case BodyPartType.TwoHand:
                        {
                            for (int i = 0; i < _slots.Length; i++)
                            {
                                if (_slots[i].bodyPartType == BodyPartType.RightHand)
                                    _slots[i].itemID = itemID;
                            }
                        }
                        break;
                    default:
                        break;
                }
            };
            for (int slotID = 0; slotID < _presenter.itemsEquippedSource.Count; slotID++)
            {
                switch ((BodyPartType)slotID)
                {
                    case BodyPartType.None:
                        break;
                    case BodyPartType.Head:
                    case BodyPartType.Top:
                    case BodyPartType.Bottom:
                    case BodyPartType.Feet:
                    case BodyPartType.RightHand:
                    case BodyPartType.LeftHand:
                        {
                            for (int i = 0; i < _slots.Length; i++)
                            {
                                if (_slots[i].bodyPartType == (BodyPartType)slotID)
                                    _slots[i].itemID = _presenter.itemsEquippedSource[slotID];
                            }
                        }
                        break;
                    case BodyPartType.TwoHand:
                        {
                            for (int i = 0; i < _slots.Length; i++)
                            {
                                if (_slots[i].bodyPartType == BodyPartType.RightHand)
                                    _slots[i].itemID = _presenter.itemsEquippedSource[slotID];
                            }
                        }
                        break;
                    default:
                        break;
                }
            }


            InputManager.instance.RegisterDownAction(KeyCode.E,
                                                     () =>
                                                     {
                                                         if (gameObject.activeSelf)
                                                             Hide();
                                                         else
                                                             Show();
                                                     });
        }
    }
}
