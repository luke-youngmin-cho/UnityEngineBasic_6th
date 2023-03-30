using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public enum Option
    {
        FixedPoints,
        BFS,
        DFS,
    }

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
    private static Transform _leftBottom;
    private static Transform _rightTop;
    private static float _nodeUnit = 1.0f;
    private static float _mapHeight;
    private static float _mapWidth;

    private static List<Transform> _fixedPath;

    public static void SetUp()
    {
        Transform pathPointsParent = GameObject.Find("Path").transform;
        Transform obstaclesParent = GameObject.Find("Nodes").transform;

        SetUp(pathPointsParent.GetComponentsInChildren<Transform>().Where(x => x != pathPointsParent),
              obstaclesParent.GetComponentsInChildren<Transform>().Where(x => x != obstaclesParent));


        SetUp(from   pathNode in pathPointsParent.GetComponentsInChildren<Transform>()
              where  pathNode != pathPointsParent
              select pathNode,
              from   obstacleNode in obstaclesParent.GetComponentsInChildren<Transform>()
              where  obstacleNode != obstaclesParent
              select obstacleNode);

    }

    public bool TryGetOptimizedPath(Transform start, Transform end, out IEnumerator<Transform> optimizedPath, Option option)
    {
        bool result = false;
        optimizedPath = null;

        switch (option)
        {
            case Option.FixedPoints:
                {
                    if (TryGetFixedPath(start, end, out optimizedPath))
                    {
                        return true;
                    }
                }
                break;
            case Option.BFS:
                break;
            case Option.DFS:
                break;
            default:
                break;
        }

        return result;
    }


    private static void SetUp(IEnumerable<Transform> pathPoints, IEnumerable<Transform> obstacles)
    {
        List<Transform> nodes = new List<Transform>();
        nodes.AddRange(pathPoints);
        nodes.AddRange(obstacles);

        nodes.Sort((a, b) => (a.position.x + a.position.z) < (b.position.x + b.position.z) ? -1 : 1);
        _leftBottom = nodes[0];
        _rightTop = nodes[nodes.Count - 1];
        _mapHeight = _rightTop.position.z - _leftBottom.position.z;
        _mapWidth = _rightTop.position.x - _leftBottom.position.x;
        _map = new MapNode[(int)(_mapHeight / _nodeUnit) + 1, (int)(_mapWidth / _nodeUnit) + 1];

        Coord tmpCoord;
        foreach (Transform node in pathPoints)
        {
            tmpCoord = GetCoord(node);
            _map[tmpCoord.y, tmpCoord.x] = new MapNode()
            {
                type = MapNodeType.Path,
                coord = tmpCoord,
                point = node,
            };
        }

        foreach (Transform node in obstacles)
        {
            tmpCoord = GetCoord(node);
            _map[tmpCoord.y, tmpCoord.x] = new MapNode()
            {
                type = MapNodeType.Obstacle,
                coord = tmpCoord,
                point = node,
            };
        }
    }

    private static Coord GetCoord(Transform point)
    {
        return new Coord(Mathf.RoundToInt((point.position.z - _leftBottom.position.z) / _nodeUnit),
                         Mathf.RoundToInt((point.position.x - _leftBottom.position.x) / _nodeUnit));
    }

    private static bool TryGetFixedPath(Transform start, Transform end, out IEnumerator<Transform> path)
    {
        _fixedPath = new List<Transform>();
        path = null;
        foreach (var customizedPath in PathInformation.instance.customizedPathList)
        {
            if (customizedPath.start == start &&
                customizedPath.end == end)
            {
                _fixedPath = customizedPath.pointList;
                path = customizedPath.pointList.GetEnumerator();
                return true;
            }
        }
        return false;
    }
}
