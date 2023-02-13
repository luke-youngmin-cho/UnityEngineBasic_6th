using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotCardDiceDouble : TarotCard
{
    public override void OnSelected()
    {
        Player.instance.speed *= 2;
    }
}
