using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class NoteSpawnManager : MonoBehaviour
{
    public static NoteSpawnManager instance;

    public bool isSpawning { get; private set; }

    public float noteFallingDistance => transform.position.y - _hitterPoint.position.y;
    public float noteFallingTime => noteFallingDistance / PlaySettings.speed;

    private Dictionary<KeyCode, NoteSpawner> _spanwers = new Dictionary<KeyCode, NoteSpawner>();
    private Queue<NoteData> _noteDataQueue;
    private float _timeMark;
    [SerializeField] private Transform _hitterPoint;



    public void StartSpawn(IEnumerable<NoteData> noteDatas)
    {
        if (isSpawning)
            return;

        // 노드데이터 정렬후 큐 생성
        _noteDataQueue = new Queue<NoteData>(noteDatas.OrderBy(note => note.time));
        _timeMark = Time.time;
        isSpawning = true;
    }

    private void Update()
    {
        if (isSpawning == false)
            return;

        while (_noteDataQueue.Count > 0)
        {
            if (_noteDataQueue.Peek().time < (Time.time - _timeMark))
            {
                _spanwers[_noteDataQueue.Dequeue().key].Spawn();
            }
            else
            {
                break;
            }
        }
    }


    private void Awake()
    {
        instance = this;
        NoteSpawner[] spawners = GetComponentsInChildren<NoteSpawner>();
        for (int i = 0; i < spawners.Length; i++)
        {
            _spanwers.Add(spawners[i].key, spawners[i]);
        }
    }
}