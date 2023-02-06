using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �ֻ��� ������ �ٲ� �� ���� �ֻ�������  UI ����
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
