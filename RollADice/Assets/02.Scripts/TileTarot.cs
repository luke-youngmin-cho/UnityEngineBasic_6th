using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTarot : Tile
{
    public override void OnHere()
    {
        base.OnHere();
        TarotUI.instance.Show();
    }
}