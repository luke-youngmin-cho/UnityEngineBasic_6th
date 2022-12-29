using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    internal class Node<T>
    {
        public T Value;
        public Node<T> Left;
        public Node<T> Right;

        public Node(T value)
        {
            Value = value;
        }
    }

    internal class MyBinaryTree<T>
    {
        public Node<T> Root
        {
            get
            {
                return _root;
            }
        }
        private Node<T> _root, _tmp1, _tmp2, _tmp3, _tmp4;

        // 삽입 알고리즘
        // O(LogN)
        public void Add(T item)
        {
            if (_root != null)
            {
                _tmp1 = _root;
                while (_tmp1 != null)
                {
                    // 내가 추가하려는 노드값이 현재 탐색 노드값보다 작은지
                    if (Comparer<T>.Default.Compare(item, _tmp1.Value) < 0)
                    {
                        if (_tmp1.Left != null)
                        {
                            _tmp1 = _tmp1.Left;
                        }
                        else
                        {
                            _tmp1.Left = new Node<T>(item);
                            return;
                        }
                    }
                    // 내가 추가하려는 노드값이 현재 탐색 노드값보다 큰지
                    else if (Comparer<T>.Default.Compare(item, _tmp1.Value) > 0)
                    {
                        if (_tmp1.Right != null)
                        {
                            _tmp1 = _tmp1.Right;
                        }
                        else
                        {
                            _tmp1.Right = new Node<T>(item);
                            return;
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"[MyBinaryTree] : {item} 은 이미 존재 합니다. 중복 허용하지않음.");
                    }
                }
            }
            else
            {
                _root = new Node<T>(item);
            }
        }

        // 탐색 알고리즘
        // 위 삽입 알고리즘을 참고하여 탐색알고리즘을 완성해 보세여
        public Node<T> Find(T item)
        {
            _tmp1 = _root;
            while (_tmp1 != null)
            {
                // 작은지 ? 
                if (Comparer<T>.Default.Compare(item, _tmp1.Value) < 0)
                    _tmp1 = _tmp1.Left;
                // 큰지 ?
                else if (Comparer<T>.Default.Compare(item, _tmp1.Value) > 0)
                    _tmp1 = _tmp1.Right;
                // 찾았다~
                else
                    break;
            }
            return _tmp1;
        }

        // 삭제 알고리즘
        // 삭제시 밸런싱 방법 : 
        // 삭제한 노드의 오른쪽자식의 가장왼쪽으로 leaf를 탐색하고
        // 더이상 왼쪽이 없더라도 오른쪽이 있으면 또 오른쪽자식으로가서 가장 왼쪽 leaf 를 탐색하는것을 반복한 후
        // 마지막으로 찾은 leaf 노드를 원래 삭제하려던 노드의 오른쪽 자식위치에다가 놓고 
        // 원래 삭제하려던 노드의 오른쪽 자식노드는 원래 삭제하려던 노드 위치에다가 놓는다.
        public bool Remove(T item)
        {
            if (_root == null)
                return false;

            bool success = false;
            int dir = 0; // 1 : right, -1 : left

            _tmp1 = _root; // 현재 탐색 노드
            _tmp2 = _root; // 현재 탐색 노드의 부모노드

            // 삭제하고자하는 노드 탐색
            while (_tmp1 != null)
            {
                // 작은지 ? 
                if (Comparer<T>.Default.Compare(item, _tmp1.Value) < 0)
                {
                    _tmp2 = _tmp1;
                    _tmp1 = _tmp1.Left;
                    dir = -1;
                }
                // 큰지 ?
                else if (Comparer<T>.Default.Compare(item, _tmp1.Value) > 0)
                {
                    _tmp2 = _tmp1;
                    _tmp1 = _tmp1.Right;
                    dir = 1;
                }
                // 찾았다~
                else
                {
                    success = true;
                    break;
                }
            }

            if (success)
            {
                // 자식이 없을 경우
                if (_tmp1.Left == null && _tmp1.Right == null)
                {
                    if (dir < 0)
                        _tmp2.Left = null;
                    else if (dir > 0)
                        _tmp2.Right = null;
                    else
                        throw new Exception($"[BinaryTree] : Wrong child");
                    _tmp1 = null;
                }
                // 왼쪽 자식만 있을 경우
                else if (_tmp1.Left != null && _tmp1.Right == null)
                {
                    if (dir < 0)
                        _tmp2.Left = _tmp1.Left;
                    else if (dir > 0)
                        _tmp2.Right = _tmp1.Left;
                    else
                        throw new Exception($"[BinaryTree] : Wrong child");
                    _tmp1 = null;
                }
                // 오른족 자식만 있을 경우
                else if (_tmp1.Left != null && _tmp1.Right == null)
                {
                    if (dir < 0)
                        _tmp2.Left = _tmp1.Right;
                    else if (dir > 0)
                        _tmp2.Right = _tmp1.Right;
                    else
                        throw new Exception($"[BinaryTree] : Wrong child");
                    _tmp1 = null;
                }
                // 자식 둘 다 있을 경우
                else
                {
                    // 트리구조에 합당한 Leaf 노드 (_tmp1 을 대체할 수 있는 leaf 노드) 탐색
                    _tmp3 = _tmp1;

                    bool done = true;
                    while (_tmp3.Right != null)
                    {
                        _tmp4 = _tmp3;
                        _tmp3 = _tmp3.Right;

                        while (_tmp3.Left != null)
                        {
                            _tmp4 = _tmp3;
                            _tmp3 = _tmp3.Left;
                            done = false;
                        }

                        if (done)
                            break;
                    }                                        

                    // tmp1 자리에 tmp3 대체
                    if (dir < 0)
                        _tmp2.Left = _tmp3;
                    else if (dir > 0)
                        _tmp2.Right = _tmp3;
                    else
                        throw new Exception($"[BinaryTree] : Wrong child");

                    // 기존 tmp1 의 자식들을 tmp3 의 자식으로 연결함
                    _tmp3.Left = _tmp1.Left;
                    if (_tmp1.Right != _tmp3)
                        _tmp3.Right = _tmp1.Right;

                    // 대체할 Leaf 노드와 그 부모의 연결 끊음
                    if (Comparer<T>.Default.Compare(_tmp3.Value, _tmp4.Value) < 0)
                        _tmp4.Left = null;
                    else if (Comparer<T>.Default.Compare(_tmp3.Value, _tmp4.Value) > 0)
                        _tmp4.Right = null;
                    else
                        throw new Exception($"[BinaryTree] : Wrong child");

                    _tmp1 = _tmp2 = _tmp3 = _tmp4 = null;
                }
            }

            return success;
        }
    }
}
