using RPG.DataModels;
using RPG.Datum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class InventorySlot : MonoBehaviour
    {
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
            }
        }
        private ItemPair _itemPair;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _num;
    }
}
