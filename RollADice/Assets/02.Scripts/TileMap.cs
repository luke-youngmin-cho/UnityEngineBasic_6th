using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 맵상의 모든 타일들을 관리하는 클래스
/// </summary>
public class TileMap : MonoBehaviour
{
    public List<Tile> tiles = new List<Tile>();
    public Tile this[int index] => tiles[index];
    public int total => tiles.Count;
}
