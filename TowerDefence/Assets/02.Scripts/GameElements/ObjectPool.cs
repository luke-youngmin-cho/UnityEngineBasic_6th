using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<ObjectPool>("ObjectPool"));
            return _instance;
        }
    }
    private static ObjectPool _instance;

    public class Element
    {
        public string name;
        public GameObject prefab;
        public int num;

        public Element(string name, GameObject prefab, int num)
        {
            this.name = name;
            this.prefab = prefab;
            this.num = num;
        }
    }

    private Dictionary<string, Stack<GameObject>> _stacks = new Dictionary<string, Stack<GameObject>>();
    private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();

    // 요소를 등록하는 함수
    public void Register(Element element)
    {
        if (_stacks.TryGetValue(element.name, out Stack<GameObject> stack) == false)
        {
            stack = new Stack<GameObject>();
            _stacks.Add(element.name, stack);
            _prefabs.Add(element.name, element.prefab);
        }

        GameObject tmp;
        for (int i = 0; i < element.num; i++)
        {
            tmp = Instantiate(element.prefab, transform);
            tmp.name = element.name;
            stack.Push(tmp);
            tmp.SetActive(false);
        }
    }

    // 요소를 가져다쓰기위한 함수
    public GameObject Take(string name)
    {
        GameObject tmp;
        if (_stacks[name].Count <= 0)
        {
            tmp = Instantiate(_prefabs[name], transform);
            tmp.name = name;
            _stacks[name].Push(tmp);
        }

        tmp = _stacks[name].Pop();
        tmp.SetActive(true);
        tmp.transform.SetParent(null);
        return tmp;
    }


    // 요소를 반납하는 함수
    public void Return(GameObject go)
    {
        if (_stacks.TryGetValue(go.name, out Stack<GameObject> stack))
        {
            stack.Push(go);
            go.SetActive(false);
            go.transform.SetParent(transform);
        }
    }
}
