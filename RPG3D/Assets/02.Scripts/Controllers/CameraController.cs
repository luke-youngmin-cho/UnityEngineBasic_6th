using RPG.InputSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace RPG.Controllers
{
    public class CameraController : Controller
    {
        public override bool controllable
        { 
            get => _controllable;
            set
            {
                _controllable = value;
                Cursor.visible = value == false;
                Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.Confined;
            }
        }
        private bool _controllable;

        [SerializeField] private Transform _target;
        [SerializeField] private float _minDistance = 3.0f;
        [SerializeField] private float _maxDistance = 30.0f;
        [SerializeField] private float _wheelSpeed = 500.0f;
        [SerializeField] private float _xMoveSpeed = 500.0f;
        [SerializeField] private float _yMoveSpeed = 250.0f;
        [SerializeField] private float _yLimitMin = 5.0f;
        [SerializeField] private float _yLimitMax = 80.0f;
        [SerializeField] private Vector3 _offset;
        private float _y, _x, _distance;

        protected override void Awake()
        {
            base.Awake();
            _distance = Vector3.Distance(transform.position, _target.position);
            _y = transform.eulerAngles.x;
            _x = transform.eulerAngles.y;

            InputManager.instance.RegisterAxisAction("Mouse X",
                                                     (value) =>
                                                     {
                                                         if (_controllable == false)
                                                             return;

                                                         _x += value * _xMoveSpeed * Time.deltaTime;
                                                         _x = ClampAngle(_x, -360.0f, 360.0f);
                                                     });
            
            InputManager.instance.RegisterAxisAction("Mouse Y",
                                                     (value) =>
                                                     {
                                                         if (_controllable == false)
                                                             return;

                                                         _y += value * _yMoveSpeed * Time.deltaTime;
                                                         _y = ClampAngle(_y, _yLimitMin, _yLimitMax);
                                                     });

            InputManager.instance.RegisterAxisAction("Mouse ScrollWheel",
                                                     (value) =>
                                                     {
                                                         if (_controllable == false)
                                                             return;

                                                         _distance -= value * _wheelSpeed * Time.deltaTime;
                                                         _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);
                                                     });

            ControllerManager.instance.Authorize(this);
        }

        private void FixedUpdate()
        {
            if (_controllable == false)
                return;

            _target.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
        }

        private void LateUpdate()
        {
            if (_controllable == false)
                return;

            transform.rotation = Quaternion.Euler(_y, _x, 0.0f);
            transform.position = _target.position - transform.rotation * Vector3.forward * _distance + _offset;
        }


        private float ClampAngle(float angle, float min, float max)
        {
            angle %= 360.0f;
            return Mathf.Clamp(angle, min, max);
        }
    }
}