using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotCardDicePackage : TarotCard
{
    [SerializeField] private int _min = 1;
    [SerializeField] private int _max = 3;

    public override void OnSelected()
    {
        DiceManager.instance.normalDice += Random.Range(_min, _max + 1);
    }
}
