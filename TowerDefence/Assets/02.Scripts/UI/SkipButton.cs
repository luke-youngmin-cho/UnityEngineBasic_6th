using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public void AddListener(UnityAction action) => _button.onClick.AddListener(action);
    public void RemoveAllListeners() => _button.onClick.RemoveAllListeners();
}
