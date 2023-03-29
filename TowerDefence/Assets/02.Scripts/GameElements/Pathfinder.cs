using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private struct Coord
    {
        public static Coord zero = new Coord(0, 0);
        public static Coord error = new Coord(-1, -1);
        public int x, y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Coord op1, Coord op2)
            => (op1.x == op2.x) && (op1.y == op2.y);

        public static bool operator !=(Coord op1, Coord op2)
            => !(op1 == op2);
    }

    private enum MapNodeType
    {
        None,
        Path,
        Obstacle
    }

    private struct MapNode
    {
        public MapNodeType type;
        public Transform point;
        public Coord coord;
    }

    private static MapNode[,] _map;
}
