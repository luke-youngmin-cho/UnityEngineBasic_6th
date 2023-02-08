using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

/// <summary>
/// 주사위를 관리
/// </summary>
public class DiceManager : MonoBehaviour
{
    #region Singleton
    public static DiceManager instance;

    private void Awake()
    {
        instance = this;
        normalDice = _normalDiceInit;
        goldenDice = _goldenDiceInit;
    }
    #endregion


    public int normalDice
    {
        get
        {
            return _normalDice;
        }
        set
        {
            _normalDice = value;
            onNormalDiceChanged?.Invoke(value);
        }
    }
    public int goldenDice
    {
        get
        {
            return _goldenDice;
        }
        set
        {
            _goldenDice = value;
            onGoldenDiceChanged?.Invoke(value);
        }
    }
    private int _normalDice;
    private int _goldenDice;
    [SerializeField] int _normalDiceInit = 20;
    [SerializeField] int _goldenDiceInit = 2;
    public event Action<int> onNormalDiceChanged;
    public event Action<int> onGoldenDiceChanged;
    public event Action<int> onRollADice;
    

    public int RollANormalDice()
    {
        // 남은 주사위 있는지 확인
        if (_normalDice <= 0)
            return -1;

        normalDice--; // 주사위 차감
        int diceValue = Random.Range(1, 7); // 랜덤한 주사위값 생성
        
        onRollADice?.Invoke(diceValue); // 구독자들에게 주사위 굴렸다는 알림 통지
        return diceValue;
    }

    public int RollAGoldenDice(int diceValue)
    {
        if (_goldenDice <= 0)
            return -1;

        goldenDice--;
        onRollADice?.Invoke(diceValue);
        return diceValue;
    }

    

    
}
