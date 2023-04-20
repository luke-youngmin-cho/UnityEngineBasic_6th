using RPG.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.UI
{
    public class UIManager : SingletonBase<UIManager>
    {
        public List<IUI> uis = new List<IUI>();
        public LinkedList<IUI> uisShown = new LinkedList<IUI>();

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
        }

        public void HideLast()
        {
            if (uisShown.Count <= 0)
                return;

            uisShown.Last.Value.Hide();
        }
    }
}
