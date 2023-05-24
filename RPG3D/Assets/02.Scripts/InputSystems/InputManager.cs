using RPG.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.InputSystems
{
    public class InputManager : SingletonMonoBase<InputManager>
    {
        public bool mouse0Trigger
        {
            get
            {
                if (_mouse0Trigger)
                {
                    _mouse0Trigger = false;
                    return true;
                }
                return false;
            }
            set
            {
                _mouse0Trigger = value;
                if (value)
                    onMouse0Triggered?.Invoke();
            }
        }
        private bool _mouse0Trigger;
        public event Action onMouse0Triggered;

        private Dictionary<KeyCode, Action> _downActions = new Dictionary<KeyCode, Action>();
        private Dictionary<KeyCode, Action> _pressActions = new Dictionary<KeyCode, Action>();
        private Dictionary<KeyCode, Action> _upActions = new Dictionary<KeyCode, Action>();

        private Dictionary<string, Action<float>> _axisActions = new Dictionary<string, Action<float>>();

        public void RegisterDownAction(KeyCode keyCode, Action action)
        {
            if (_downActions.ContainsKey(keyCode))
            {
                _downActions[keyCode] += action;
            }
            else
            {
                _downActions.Add(keyCode, action);
            }
        }

        public void RegisterPressAction(KeyCode keyCode, Action action)
        {
            if (_pressActions.ContainsKey(keyCode))
            {
                _pressActions[keyCode] += action;
            }
            else
            {
                _pressActions.Add(keyCode, action);
            }
        }

        public void RegisterUpAction(KeyCode keyCode, Action action)
        {
            if (_upActions.ContainsKey(keyCode))
            {
                _upActions[keyCode] += action;
            }
            else
            {
                _upActions.Add(keyCode, action);
            }
        }

        public void RegisterAxisAction(string axisName, Action<float> action)
        {
            if (_axisActions.ContainsKey(axisName))
            {
                _axisActions[axisName] += action;
            }
            else
            {
                _axisActions.Add(axisName, action);
            }
        }

        protected override void Init()
        {
            base.Init();
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                mouse0Trigger = true;

            foreach (var pair in _downActions)
            {
                if (Input.GetKeyDown(pair.Key))
                    pair.Value();
            }

            foreach (var pair in _pressActions)
            {
                if (Input.GetKey(pair.Key))
                    pair.Value();
            }

            foreach (var pair in _upActions)
            {
                if (Input.GetKeyUp(pair.Key))
                    pair.Value();
            }

            foreach (var pair in _axisActions)
            {
                _axisActions[pair.Key](Input.GetAxis(pair.Key));
            }
        }
    }
}