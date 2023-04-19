using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public interface IUI
    {
        int sortingOrder { set; }

        event Action onShow;
        event Action onHide;

        void Show();
        void Hide();
        void ShowUnmanaged();
        void HideUnmanaged();
    }


}