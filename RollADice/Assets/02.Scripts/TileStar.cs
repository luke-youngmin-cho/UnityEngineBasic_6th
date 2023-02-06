using UnityEngine;
using TMPro;

public class TileStar : Tile
{
    public int starValue = 3;
    [SerializeField] private TMP_Text _valueText;

    public override void OnHere()
    {
        base.OnHere();
        IncreaseStarValue();
    }

    private void Awake()
    {
        _valueText.text = starValue.ToString();
    }

    private void IncreaseStarValue()
    {
        starValue++;
        _valueText.text = starValue.ToString();
    }
}