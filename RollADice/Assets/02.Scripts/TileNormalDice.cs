using UnityEngine;

public class TileNormalDice : Tile
{
    public override void OnHere()
    {
        base.OnHere();
        IncreaseNormalDice();
    }

    private void IncreaseNormalDice()
    {
        DiceManager.instance.normalDice++;
    }
}