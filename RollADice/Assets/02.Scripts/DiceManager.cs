using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    #region Singleton
    public static DiceManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public const int DIRECTION_FORWARD = 1;
    public const int DIRECTION_BACKWARD = -1;
    public int direction = DIRECTION_FORWARD;

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
    public event Action<int> onNormalDiceChanged;
    public event Action<int> onGoldenDiceChanged;
    public event Action<int> onRollADice;
    private int _currentTileIndex;
    [SerializeField] private TileMap _tileMap;
    private List<TileStar> _tileStars = new List<TileStar>();

    public void RollANormalDice()
    {
        // ���� �ֻ��� �ִ��� Ȯ��
        if (_normalDice <= 0)
            return;

        _normalDice--; // �ֻ��� ����
        int diceValue = Random.Range(1, 7); // ������ �ֻ����� ����
        
        onRollADice?.Invoke(diceValue); // �����ڵ鿡�� �ֻ��� ���ȴٴ� �˸� ����
    }

    public void RollAGoldenDice(int diceValue)
    {
        if (_goldenDice <= 0)
            return;

        _goldenDice--;
        onRollADice?.Invoke(diceValue);
    }

    private void Start()
    {
        foreach (Tile tile in _tileMap.tiles)
        {
            if (tile is TileStar)
            {
                _tileStars.Add((TileStar)tile);
            }
        }
    }

    private void EarnStarValue(int prev, int diceValue)
    {
        int sum = 0;
        foreach (TileStar tileStar in _tileStars)
        {
            if (prev + diceValue > _tileMap.total)
            {
                if (tileStar.index <= prev)
                {
                    if (tileStar.index <= prev + diceValue - _tileMap.total)
                    {
                        sum += tileStar.starValue;
                    }
                }
                else
                {
                    sum += tileStar.starValue;
                }
            }
            else
            {
                // �� ����ĭ�� �ֻ����� ���� ���� ���� �ִ���
                if (tileStar.index > prev &&
                    tileStar.index <= prev + diceValue)
                {
                    sum += tileStar.starValue;
                }
            }
        }
    }
}
