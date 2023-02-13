using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton
    public static Player instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public int star
    {
        get
        {
            return _star;
        }
        private set
        {
            _star = value;
            onStarChanged?.Invoke(value);
        }
    }
    private int _star;
    public event Action<int> onStarChanged;
    public float starGain = 1.0f;

    public const int DIRECTION_FORWARD = 1;
    public const int DIRECTION_BACKWARD = -1;
    public int direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
            onDirectionChanged?.Invoke(value);
        }
    }
    private int _direction = DIRECTION_FORWARD;
    public event Action<int> onDirectionChanged;

    public float speed = 1.0f;

    [SerializeField] private TileMap _tileMap;
    private List<TileStar> _tileStars = new List<TileStar>();
    private int _currentTileIndex = -1;


    public void Move(int diceValue)
    {
        diceValue = (int)(diceValue * speed);

        // 정방향
        if (direction == DIRECTION_FORWARD)
        {
            // 샛별획득
            EarnStarValue(_currentTileIndex, diceValue);

            // 다음 칸 계산
            _currentTileIndex += diceValue;
            _currentTileIndex %= _tileMap.total;
        }
        // 역방향
        else if (direction == DIRECTION_BACKWARD)
        {
            _currentTileIndex -= diceValue;
            if (_currentTileIndex < 0)
                _currentTileIndex += _tileMap.total;

            direction = DIRECTION_FORWARD;
        }

        // 플레이어 실제 이동
        transform.position = _tileMap[_currentTileIndex].transform.position;
        _tileMap[_currentTileIndex].OnHere();

        speed = 1.0f;
    }

    public void MoveBackToOrigin()
    {
        transform.position = _tileMap[0].transform.position;
        _currentTileIndex = 0;
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
                // 이 샛별칸이 주사위를 굴린 범위 내에 있는지
                if (tileStar.index > prev &&
                    tileStar.index <= prev + diceValue)
                {
                    sum += tileStar.starValue;
                }
            }
        }
        sum = (int)(sum * starGain);
        star += sum;
        starGain = 1.0f;
    }
}