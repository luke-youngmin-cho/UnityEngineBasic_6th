using UnityEngine;

namespace RPG.Controllers
{
    public abstract class Controller : MonoBehaviour, IController
    {
        public virtual bool controllable { get; set; }

        protected virtual void Awake()
        {
            ControllerManager.instance.Register(this);
        }
    }
}
