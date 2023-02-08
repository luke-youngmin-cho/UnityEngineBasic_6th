using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// 주사위 갯수가 바뀔 때 마다 주사위갯수  UI 갱신
/// </summary>
public class DicePanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _normalDice;
    [SerializeField] private TMP_Text _goldenDice;
    [SerializeField] private Button _normalDiceButton;
    [SerializeField] private Button _goldenDiceButton;
    [SerializeField] private GameObject _reverseIcons;
    [SerializeField] private GameObject _goldenDiceSelectionPanel;
    [SerializeField] private List<Button> _goldenDiceSelectionButtons;

    private void Start()
    {
        DiceManager diceManager = DiceManager.instance;
        diceManager.onNormalDiceChanged += (dice) =>
        {
            _normalDiceButton.interactable = dice > 0;
            _normalDice.text = dice.ToString();
        };
        diceManager.onGoldenDiceChanged += (dice) =>
        {
            _goldenDiceButton.interactable = dice > 0;
            _goldenDice.text = dice.ToString();
        };

        // 일반주사위버튼 클릭시
        // 주사위굴리고, 나온 눈금으로 애니메이션 재생하고 , 애니메이션끝날때 플레이어를 해당 눈금만큼 이동시킴
        _normalDiceButton.onClick.AddListener(() =>
        {
            if (DiceAnimationUI.instance.isBusy)
                return;

            int diceValue = diceManager.RollANormalDice();
            DiceAnimationUI.instance.Play(diceValue, () => Player.instance.Move(diceValue));
        });

        // 황금주사위버튼 클릭시
        _goldenDiceButton.onClick.AddListener(() =>
        {
            _goldenDiceSelectionPanel.SetActive(true);
        });

        // 황금주사위 선택 버튼들
        for (int i = 0; i < _goldenDiceSelectionButtons.Count; i++)
        {
            // 주의. 반복문에서 대리자에 구독할때 인덱스용 변수 (i) 를 쓰게되면 모든 구독함수가 i 를 참조하므로
            // 변수를 직접 사용하지않고 변수의 현재 값을 가져와서 사용해야함.
            int selectedValue = i + 1;

            _goldenDiceSelectionButtons[i].onClick.AddListener(() =>
            {
                if (DiceAnimationUI.instance.isBusy)
                    return;
                                
                int diceValue = diceManager.RollAGoldenDice(selectedValue);
                _goldenDiceSelectionPanel.SetActive(false);
                DiceAnimationUI.instance.Play(diceValue, () => Player.instance.Move(diceValue));
            });
        }


        // 방향전환에 따른 벌칙 아이콘 활성
        Player.instance.onDirectionChanged += (value) =>
        {
            _reverseIcons.SetActive(value == Player.DIRECTION_BACKWARD);
        };

        _normalDice.text = diceManager.normalDice.ToString();
        _goldenDice.text = diceManager.goldenDice.ToString();
        _normalDiceButton.interactable = diceManager.normalDice > 0;
        _goldenDiceButton.interactable = diceManager.goldenDice > 0;
        _reverseIcons.SetActive(Player.instance.direction == Player.DIRECTION_BACKWARD);
    }
}
