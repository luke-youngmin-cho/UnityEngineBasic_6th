using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // System.Collections
            // .net 제공 자료구조 클래스 및 인터페이스
            //===============================================================

            // ArrayList
            // object 타입 동적배열
            //---------------------------------------------------------
            ArrayList arrayList = new ArrayList();
            arrayList.Add("철수");
            arrayList.Add(123);
            arrayList.Add('B');
            arrayList.Remove("철수");
            Console.WriteLine(arrayList[arrayList.IndexOf(123)]);

            // Hashtable
            // object 타입 해쉬테이블
            //---------------------------------------------------------
            Hashtable hashtable = new Hashtable();
            hashtable.Add("철수", 90);
            hashtable["철수"] = 80;
            hashtable.Remove("철수");

            // System.Collections.Generic
            // .net 제공 자료구조 제네릭 클래스 및 인터페이스
            //===============================================================

            // List<T> 
            // 제네릭 동적 배열
            //----------------------------------------------------------
            List<int> list = new List<int>();
            list.Add(3);
            list.Find(x => x == 3);
            list.Remove(3);
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }

            // LinkedList<T>
            // 제네릭 연결 리스트
            //----------------------------------------------------------
            LinkedList<float> linkedList = new LinkedList<float>();
            linkedList.AddLast(3.0f);
            linkedList.AddBefore(linkedList.Find(3.0f), 4.0f);
            linkedList.Remove(4.0f);
            Console.WriteLine(linkedList.First.Value);
            linkedList.Contains(5.0f);

            LinkedListNode<float> node = linkedList.First;
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }

            // Dictionary<TKey, TValue>
            // 제네릭 해시 테이블
            //----------------------------------------------------------
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(5, "철수");
            dictionary[1] = "영희";
            string value;
            if (dictionary.TryGetValue(2, out value))
            {
                Console.WriteLine(value);
                dictionary.Remove(2);
            }
            if (dictionary.ContainsKey(2))
            {

            }
            if (dictionary.ContainsValue("철수"))
            {

            }
        }
    }
}
