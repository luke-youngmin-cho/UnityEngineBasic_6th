using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    internal class Node<T>
    {
        public T Value;
        public Node<T> Next;
        public Node<T> Prev;

        public Node(T value)
        {
            Value = value;
        }
    }

    internal class MyLinkedList<T>
    {
        public Node<T> First => _first;
        public Node<T> Last => _last;
        private Node<T> _first, _last, _tmp1, _tmp2;

        public int Count
        {
            get
            {
                int count = 0;
                _tmp1 = _first;
                while (_tmp1 != null)
                {
                    count++;
                    _tmp1 = _tmp1.Next;
                }
                return count;
            }
        }

        // 삽입 알고리즘 
        // O(1)
        public void AddFirst(T value)
        {
            _tmp1 = new Node<T>(value);

            // 노드가 현재 하나이상 존재한다면
            if (_first != null)
            {
                _tmp1.Next = _first;
                _first.Prev = _tmp1;
            }
            // 노드가 하나도 없다면
            if (_last == null)
            {
                _last = _tmp1;
            }
            _first = _tmp1;
        }

        // 작성 해 보슈
        public void AddLast(T value)
        {

        }

        public void AddBefore(Node<T> node, T value)
        {

        }

        public void AddAfter(Node<T> node, T value)
        {

        }

        public Node<T> Find(int value)
        {
            return null;
        }

        public Node<T> FindLast(int value)
        {
            return null;
        }

        public bool Remove(int value)
        {
            return false;
        }

        public bool RemoveLast(int value)
        {
            return false;
        }
    }
}
