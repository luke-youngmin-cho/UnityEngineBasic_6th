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
    }
    #endregion

    [SerializeField] private List<TarotCard> _cards;

    public void Show()
    {
        // 기존카드 다 뒤집고(비활성화)
        foreach (TarotCard card in _cards)
        {
            card.gameObject.SetActive(false);
        }

        // 카드 전부 무작위로 섞음
        IEnumerable<TarotCard> shuffled = _cards.OrderBy((x) => Guid.NewGuid());

        // 카드 세개 뽑음
        int count = 0;
        foreach (TarotCard card in shuffled)
        {
            card.gameObject.SetActive(true);
            count++;

            if (count >= 3)
                break;
        }
    }
}
