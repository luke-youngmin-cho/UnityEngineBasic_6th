using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private int _level;

    private void Awake()
    {
        GetComponent<Button>()
            .onClick
            .AddListener(() =>
            {
                GameManager.instance.levelSelected = _level;
                GameManager.instance.StartGame();
            });
    }
}
