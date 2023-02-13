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
        // ����ī�� �� ������(��Ȱ��ȭ)
        foreach (TarotCard card in _cards)
        {
            card.gameObject.SetActive(false);
        }

        // ī�� ���� �������� ����
        IEnumerable<TarotCard> shuffled = _cards.OrderBy((x) => Guid.NewGuid());

        // ī�� ���� ����
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
