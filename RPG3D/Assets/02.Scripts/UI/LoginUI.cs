using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RPG.UI
{
    public class LoginUI : UIMonoBehaviour
    {
        [SerializeField] private TMP_InputField _id;
        [SerializeField] private TMP_InputField _pw;
        [SerializeField] private Button _confirm;

        protected override void Init()
        {
            base.Init();
            _confirm.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(_id.text) ||
                    string.IsNullOrEmpty(_pw.text))
                {
                    UIManager.instance
                        .Get<WarningWindowUI>()
                        .Show(null, "Wrong ID/PW", 2.0f);
                }
                else
                {
                    SceneManager.LoadScene("SampleField1");
                }
            });
        }
    }
}