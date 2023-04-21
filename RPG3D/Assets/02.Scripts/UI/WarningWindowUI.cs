using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI 
{
    public class WarningWindowUI : UIMonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _message;

        public void Show(string title, string message, float autoHideDelay)
        {
            if (autoHideDelay <= 0)
                return;

            _title.text = title;
            _message.text = message;
            base.Show();
            Invoke("Hide", autoHideDelay);
        }
    }
}