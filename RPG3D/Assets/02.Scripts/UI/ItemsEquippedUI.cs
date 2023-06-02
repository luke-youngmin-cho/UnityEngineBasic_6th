using RPG.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.UI
{
    public class ItemsEquippedUI : UIMonoBehaviour
    {
        [SerializeField] private ItemsEquippedSlot[] _slots;

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
    }
}
