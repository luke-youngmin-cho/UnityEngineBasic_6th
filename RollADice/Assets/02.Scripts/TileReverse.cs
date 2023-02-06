using UnityEngine;

public class TileReverse : Tile
{
    public override void OnHere()
    {
        base.OnHere();
        ReverseDirection();
    }

    private void ReverseDirection()
    {
        DiceManager.instance.direction = DiceManager.DIRECTION_BACKWARD;
    }
}