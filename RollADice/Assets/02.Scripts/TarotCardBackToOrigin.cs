using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotCardBackToOrigin : TarotCard
{
    public override void OnSelected()
    {
        Player.instance.MoveBackToOrigin();
    }
}
