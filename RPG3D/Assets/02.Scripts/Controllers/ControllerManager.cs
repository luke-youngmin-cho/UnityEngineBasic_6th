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

        public T Get<T>() where T : IController
        {
            return (T)_controllers.Find(controller => controller is T);
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
