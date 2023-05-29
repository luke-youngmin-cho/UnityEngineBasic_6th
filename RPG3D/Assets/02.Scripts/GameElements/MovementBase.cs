using UnityEngine;

namespace RPG.GameElements
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public abstract class MovementBase : MonoBehaviour
    {
        public enum Mode
        {
            None,
            Manual,
            RootMotion,
        }

        public Mode mode
        {
            get
            {
                return _mode;
            }
            set
            {
                if (_mode == value)
                    return;

                inertia = rb.velocity;
                _mode = value;
            }
        }
        private Mode _mode;

        protected Vector3 inertia;
        protected Rigidbody rb;
        protected Animator animator;
        private float _drag;
        public abstract float v { get; }
        public abstract float h { get; }
        public abstract float gain { get; }

        public virtual void SetMove(float horizontal, float vertical, float gain) { }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            mode = Mode.RootMotion;
            _drag = rb.drag;
        }

        private void Update()
        {
            if (_mode == Mode.RootMotion)
            {
                animator.SetFloat("h", h * gain);
                animator.SetFloat("v", v * gain);
            }
        }

        private void FixedUpdate()
        {
            if (_mode == Mode.Manual)
            {
                rb.position += inertia * Time.fixedDeltaTime;
                inertia = Vector3.Lerp(inertia, Vector3.zero, _drag * Time.fixedDeltaTime);
            }
        }
    }
}