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
        private Node<T> _root, _tmp1, _tmp2;

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

        }
    }
}
