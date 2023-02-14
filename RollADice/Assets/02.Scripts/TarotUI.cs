using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TarotUI : MonoBehaviour
{
    #region Singleton
    public static TarotUI instance;
    private void Awake()
    {
        instance = this;
        _content.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
    #endregion

    [SerializeField] private RectTransform _content;
    [SerializeField] private List<TarotCard> _cards;

    public void Show()
    {
        // 카드 전부 무작위로 섞음
        IEnumerable<TarotCard> shuffled = _cards.OrderBy((x) => Guid.NewGuid());

        // 카드 세개 뽑음
        int count = 0;
        foreach (TarotCard card in shuffled)
        {
            card.gameObject.SetActive(count < 3);
            count++;
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
