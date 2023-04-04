using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private LevelData _levelData;
    private int _stage;
    private Vector3 _offset = Vector3.up * 0.25f;
    private SkipButton[] _skipButtons;
    [SerializeField] private SkipButton _skipButtonPrefab;

    public void SpawnNext()
    {
        DeactiveAllSkipButtons();
        StartCoroutine(E_Spawn(_levelData.stageDataList[_stage++]));
        Invoke("ActiveSkipButtons", 2.0f);
    }

    private void Awake()
    {
        _levelData = GameManager.instance.levelData;
        for (int i = 0; i < _levelData.stageDataList.Count; i++)
        {
            for (int j = 0; j < _levelData.stageDataList[i].spawnDataList.Count; j++)
            {
                ObjectPool.instance.Register(new ObjectPool.Element(_levelData.stageDataList[i].spawnDataList[j].prefab.name,
                                                                    _levelData.stageDataList[i].spawnDataList[j].prefab,
                                                                    _levelData.stageDataList[i].spawnDataList[j].num));
            }
        }
    }

    private void Start()
    {
        _skipButtons = new SkipButton[PathInformation.instance.startPoints.Count];
        for (int i = 0; i < _skipButtons.Length; i++)
        {
            _skipButtons[i] = Instantiate(_skipButtonPrefab,
                                          PathInformation.instance.startPoints[i].position +
                                          Vector3.up * 1.5f,
                                          _skipButtonPrefab.transform.rotation);
            _skipButtons[i].AddListener(() =>
            {
                DeactiveAllSkipButtons();
                SpawnNext();
            });
            _skipButtons[i].gameObject.SetActive(false);
        }
        ActiveSkipButtons();
    }

    private IEnumerator E_Spawn(StageData stageData)
    {
        int count = stageData.spawnDataList.Count;
        float[] delayTimeMarks = new float[count];
        float[] termTimeMarks = new float[count];
        int[] counters = new int[count];

        for (int i = 0; i < count; i++)
        {
            delayTimeMarks[i] = stageData.spawnDataList[i].startDelay;
        }

        while (true)
        {
            bool isFinished = true;
            for (int i = 0; i < count; i++)
            {
                // 소환할것이 남았다면
                if (counters[i] < stageData.spawnDataList[i].num)
                {
                    isFinished = false;

                    // 소환 지연 타이머 확인
                    if (Time.time - delayTimeMarks[i] > stageData.spawnDataList[i].startDelay)
                    {
                        // 소환 주기 타이머 확인
                        if (Time.time - termTimeMarks[i] > stageData.spawnDataList[i].term)
                        {
                            //Enemy enemy = Instantiate(stageData.spawnDataList[i].prefab,
                            //                          PathInformation.instance.startPoints[stageData.spawnDataList[i].startPointIndex].position +
                            //                          stageData.spawnDataList[i].prefab.transform.position +
                            //                          _offset,
                            //                          Quaternion.identity)
                            //                .GetComponent<Enemy>();

                            Enemy enemy = ObjectPool.instance.Take(stageData.spawnDataList[i].prefab.name).GetComponent<Enemy>();
                            enemy.onHpMin += () => ObjectPool.instance.Return(enemy.gameObject);
                            enemy.transform.position = PathInformation.instance.startPoints[stageData.spawnDataList[i].startPointIndex].position +
                                                      stageData.spawnDataList[i].prefab.transform.position +
                                                      _offset;
                            enemy.SetPath(PathInformation.instance.startPoints[stageData.spawnDataList[i].startPointIndex],
                                          PathInformation.instance.endPoints[stageData.spawnDataList[i].endPointIndex]);
                            counters[i]++;
                            termTimeMarks[i] = Time.time;
                        }
                    }
                }
            }

            if (isFinished)
                break;

            yield return null;
        }
    }

    private void ActiveSkipButtons()
    {
        if (_stage >= _levelData.stageDataList.Count)
            return;

        List<SpawnData> spawnDataList = _levelData.stageDataList[_stage].spawnDataList;
        for (int i = 0; i < spawnDataList.Count; i++)
        {
            _skipButtons[spawnDataList[i].startPointIndex].gameObject.SetActive(true);
        }
    }

    private void DeactiveAllSkipButtons()
    {
        for (int i = 0; i < _skipButtons.Length; i++)
        {
            _skipButtons[i].gameObject.SetActive(false);
        }
    }
}
