using UnityEngine;

public class TileGoldenDice : Tile
{
    public override void OnHere()
    {
        base.OnHere();
        IncreaseGoldenDice();
    }

    private void IncreaseGoldenDice()
    {
        DiceManager.instance.goldenDice++;
    }
}