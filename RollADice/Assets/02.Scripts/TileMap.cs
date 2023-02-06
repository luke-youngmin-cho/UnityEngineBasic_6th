using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public List<Tile> tiles = new List<Tile>();
    public Tile this[int index] => tiles[index];
    public int total => tiles.Count;
}
