using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 플레이어의 샛별점수를 나타내는 UI
/// </summary>
public class StarPanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _star;

    private void Start()
    {
        Player.instance.onStarChanged += (value) =>
        {
            _star.text = value.ToString();
        };
        _star.text = Player.instance.star.ToString();
    }
}
