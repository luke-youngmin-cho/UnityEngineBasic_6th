using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.UI
{
    [RequireComponent(typeof(Canvas))]
    public class UIMonoBehaviour : MonoBehaviour, IUI
    {
        protected UIManager manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = UIManager.instance;
                    _manager.Register(this);
                }
                return _manager;
            }
        }
        private UIManager _manager;

        public int sortingOrder
        {
            set => _canvas.sortingOrder = value;
        }
        private Canvas _canvas;


        public event Action onShow;
        public event Action onHide;

        public void Show()
        {
            gameObject.SetActive(true);
            _manager.Push(this);
            onShow?.Invoke();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _manager.Pop(this);
            onHide?.Invoke();
        }

        public void ShowUnmanaged()
        {
            gameObject.SetActive(true);
            sortingOrder = _manager.uisShown.Count;
            onShow?.Invoke();
        }

        public void HideUnmanaged()
        {
            gameObject.SetActive(false);
            sortingOrder = 0;
            onHide?.Invoke();
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void OnDestroy()
        {
            _manager.Remove(this);
        }
    }
}
