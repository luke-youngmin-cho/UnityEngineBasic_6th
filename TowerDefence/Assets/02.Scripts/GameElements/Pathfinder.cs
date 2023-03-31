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

        public static Coord operator +(Coord op1, Coord op2)
            => new Coord(op1.x + op2.x, op1.y + op1.y);

        public static Coord operator -(Coord op1, Coord op2)
            => new Coord(op1.x - op2.x, op1.y - op1.y);
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
    private static int[,] _searchPattern = new int[2, 4]
    {
        { -1, 0, 1, 0},
        {  0,-1, 0, 1}
    };

    private static List<Transform> _fixedPath;
    private static List<Transform> _dfsPath;

    public static void SetUp()
    {
        Transform pathPointsParent = GameObject.Find("Path").transform;
        Transform obstaclesParent = GameObject.Find("Nodes").transform;

        SetUp(pathPointsParent.GetComponentsInChildren<Transform>().Where(x => x != pathPointsParent),
              obstaclesParent.GetComponentsInChildren<Transform>().Where(x => x != obstaclesParent));


        //SetUp(from   pathNode in pathPointsParent.GetComponentsInChildren<Transform>()
        //      where  pathNode != pathPointsParent
        //      select pathNode,
        //      from   obstacleNode in obstaclesParent.GetComponentsInChildren<Transform>()
        //      where  obstacleNode != obstaclesParent
        //      select obstacleNode);

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
                {
                    if (TryGetDFSPath(start, end, out optimizedPath))
                    {
                        return true;
                    }
                }
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
        Debug.Log($"맵 세팅중 .. 사이즈 : {_map.GetLength(1)}, {_map.GetLength(0)}");

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
        return new Coord(Mathf.RoundToInt((point.position.x - _leftBottom.position.x) / _nodeUnit),
                         Mathf.RoundToInt((point.position.z - _leftBottom.position.z) / _nodeUnit));
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

    private static bool TryGetDFSPath(Transform start, Transform end, out IEnumerator<Transform> path)
    {
        bool[,] visited = new bool[_map.GetLength(0), _map.GetLength(1)];
        Stack<Coord> stack = new Stack<Coord>();
        List<KeyValuePair<Coord, Coord>> pairs = new List<KeyValuePair<Coord, Coord>>();
        Coord startCoord = GetCoord(start);
        Coord endCoord = GetCoord(end);
        stack.Push(startCoord);

        while (stack.Count > 0)
        {
            Coord current = stack.Pop();

            // 도착 체크
            if (current == endCoord)
            {
                path = BacktrackPath(startCoord, endCoord, pairs).GetEnumerator();
                return true;
            }
            else
            {
                for (int i = _searchPattern.GetLength(1) - 1; i >= 0; i--)
                {
                    Coord next = current + new Coord(_searchPattern[1, i], _searchPattern[0, i]);

                    // 맵 범위 초과 체크
                    if (next.x < 0 || next.x >= _map.GetLength(1) ||
                        next.y < 0 || next.y >= _map.GetLength(0))
                        continue;

                    // 방문여부체크
                    if (visited[next.y, next.x])
                        continue;

                    // 장애물 체크
                    if (_map[next.y, next.x].type == MapNodeType.Obstacle)
                        continue;

                    stack.Push(next);
                    pairs.Add(new KeyValuePair<Coord, Coord>(current, next));
                }
            }
        }

        path = null;
        return false;
    }

    private static List<Transform> BacktrackPath(Coord start, Coord end, List<KeyValuePair<Coord, Coord>> pairs)
    {
        List<Transform> result = new List<Transform>();

        int index = pairs.FindLastIndex(pair => pair.Value == end);

        if (index < 0)
            throw new System.Exception("[Pathfinder] : 경로를 역추적할 수 없습니다. 탐색이 잘못되었습니다");

        result.Add(GetPoint(end));
        while (index > 0 &&
               pairs[index].Key != start)
        {
            result.Add(GetPoint(pairs[index].Key));
            index = pairs.FindLastIndex(pair => pair.Value == pairs[index].Key);
        }
        result.Add(GetPoint(start));
        result.Reverse();
        return result;
    }

    private static Transform GetPoint(Coord coord)
        => _map[coord.y, coord.x].point;
}
