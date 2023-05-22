using RPG.GameSystems;
using System;
using System.Collections;
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

        protected virtual void Awake()
        {
            manager.Register(this);
            _canvas = GetComponent<Canvas>();
            MainSystem.instance.StartCoroutine(E_Init());
        }

        private IEnumerator E_Init()
        {
            yield return new WaitUntil(() => RPG.GameSystems.SceneInitializer.IsInitialized);
            Init();
        }

        protected virtual void Init()
        {

        }

        private void OnDestroy()
        {
            _manager.Remove(this);
        }
    }
}
