using RPG.Tools;
using System.Collections.Generic;

namespace RPG.Controllers
{
    public class ControllerManager : SingletonBase<ControllerManager>
    {
        private List<IController> _controllers = new List<IController>();
        private IController _authorized;

        public void Register(IController controller)
        {
            _controllers.Add(controller);
            controller.controllable = false;
        }

        // 숙제. 
        // 현재 ControllerManager 및 UIManager 의 Get<T>(), TryGet<T>() 는 
        // Find() / FindIndex() 를 사용해서 특정 타입 요소를 찾아서 반환하므로 O(n) 이다. 
        // 매 프레임마다 O(n) 연산을 하는것은 비효율적이므로 
        // Get<T>() 와 TryGet<T>() 를 O(1) 로 사용할 수 있도록 코드를 수정하시오.
        public T Get<T>() where T : IController
        {
            return (T)_controllers.Find(controller => controller is T);
        }

        public bool TryGet<T>(out T controller)
        {
            int index = _controllers.FindIndex(x => x is T);
            if (index >= 0)
            {
                controller = (T)_controllers[index];
                return true;
            }

            controller = default(T);
            return false;
        }

        public bool IsAuthorized<T>() where T : IController
        {
            return _authorized is T;
        }

        public void Authorize(IController controller)
        {
            foreach (IController sub in _controllers)
            {
                sub.controllable = sub == controller;
            }
            _authorized = controller;
        }

        public void Authorize<T>() where T : IController
        {
            foreach (IController controller in _controllers)
            {
                if (controller is T)
                {
                    controller.controllable = true;
                    _authorized = controller;
                }
                else
                {
                    controller.controllable = false;
                }
            }
        }

        public void Dismiss(IController controller)
        {
            if (_authorized == controller)
            {
                controller.controllable = false;
                _authorized = null;
            }
            else
            {
                throw new System.Exception($"[ControllerManager] : Tried to dismiss wrong controller. {controller} wasn't authorized");
            }
        }

        public void Dismiss<T>() where T : IController
        {
            if (_authorized is T)
            {
                _authorized.controllable = false;
                _authorized = null;
            }
            else
            {
                throw new System.Exception($"[ControllerManager] : Tried to dismiss wrong controller. {typeof(T)} wasn't authorized");
            }
        }
    }
}
