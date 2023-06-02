using RPG.Datum;
using RPG.GameElements.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class ItemsEquippedSlot : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        public BodyPartType bodyPartType;
        public int itemID
        {
            get => _itemID;
            set
            {
                if (value > 0)
                {
                    _icon.sprite = ItemInfoAssets.instance[value].icon;
                }
                else
                {
                    _icon.sprite = null;
                }
                _itemID = value;
            }
        }
        private int _itemID;
    }
}
