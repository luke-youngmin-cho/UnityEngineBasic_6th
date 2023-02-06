using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 주사위 갯수가 바뀔 때 마다 주사위갯수  UI 갱신
/// </summary>
public class DicePanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _normalDice;
    [SerializeField] private TMP_Text _goldenDice;

    private void Start()
    {
        DiceManager diceManager = DiceManager.instance;
        diceManager.onNormalDiceChanged += (dice) => _normalDice.text = dice.ToString();
        diceManager.onGoldenDiceChanged += (dice) =>
        {
            _goldenDice.text = dice.ToString();
        };
        _normalDice.text = diceManager.normalDice.ToString();
        _goldenDice.text = diceManager.goldenDice.ToString();
    }
}
