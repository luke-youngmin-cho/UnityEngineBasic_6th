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

    [SerializeField] private TileMap _tileMap;
    private List<TileStar> _tileStars = new List<TileStar>();


    public void Move(int diceValue)
    {
        //transform.Translate(target, Space.World);
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
    }
}