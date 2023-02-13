using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotCardStarDouble : TarotCard
{
    public override void OnSelected()
    {
        Player.instance.starGain = 2.0f;
    }
}
