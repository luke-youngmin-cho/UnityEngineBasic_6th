using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace RPG.UI
{ 
    public class ConfirmWindowWithInputFieldUI : UIMonoBehaviour
    {
        [SerializeField] private TMP_Text _content;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _confirm;
        [SerializeField] private Button _cancel;

        public int GetInput()
        {
            return Convert.ToInt32(_inputField.text);
        }

        public void Show(UnityAction onConfirm = null, UnityAction onCancel = null, string content = "")
        {
            _confirm.onClick.RemoveAllListeners();
            _cancel.onClick.RemoveAllListeners();

            if (onConfirm != null)
            {
                _confirm.enabled = true;
                _confirm.onClick.AddListener(onConfirm);
            }
            else
            {
                _confirm.enabled = false;
            }

            if (onCancel != null)
                _cancel.onClick.AddListener(onCancel);
            _cancel.onClick.AddListener(Hide);

            _content.text = content;
            base.Show();
        }
    }
}