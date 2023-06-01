﻿using RPG.Controllers;
using RPG.GameElements;
using RPG.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace RPG.UI
{
    public class UIManager : SingletonBase<UIManager>
    {
        public List<IUI> uis = new List<IUI>();
        public LinkedList<IUI> uisShown = new LinkedList<IUI>();

        public T Get<T>()
        {
            try
            {
                return (T)uis.Find(ui => ui is T);
            }
            catch
            {
                throw new Exception($"[UIManager] : {typeof(T)} has not registered");
            }
        }

        public bool TryGet<T>(out T ui)
        {
            int index = uis.FindIndex(x => x is T);
            if (index >= 0)
            {
                ui = (T)uis[index];
                return true;
            }

            ui = default(T);
            return false;
        }

        public void Register(IUI ui)
        {
            uis.Add(ui);
        }

        public bool Remove(IUI ui)
        {
            if (uis.Remove(ui))
            {
                uisShown.Remove(ui);
                return true;
            }

            return false;
        }

        public void Push(IUI ui)
        {
            if (uisShown.Count > 0 &&
                uisShown.Last.Value == ui)
            {
                return;
            }

            uisShown.Remove(ui);
            uisShown.AddLast(ui);
            ui.sortingOrder = uisShown.Count;

            ControllerManager.instance.Dismiss<PlayerController>();
        }

        public void Pop(IUI ui)
        {
            if (uisShown.Remove(ui))
            {
                ui.sortingOrder = 0;
            }
            else
            {
                throw new Exception($"[UIManager] : 이미 비활성화되어있는 {ui} 를 비활성화하려고 시도했습니다.");
            }

            if (uisShown.Count == 0)
            {
                ControllerManager.instance.Authorize<PlayerController>();
            }
        }

        public void HideLast()
        {
            if (uisShown.Count <= 0)
                return;

            uisShown.Last.Value.Hide();
        }

        
    }
}
