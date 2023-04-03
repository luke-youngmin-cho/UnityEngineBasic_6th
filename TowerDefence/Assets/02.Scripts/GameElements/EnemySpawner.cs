using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private LevelData _levelData;
    private int _stage;

    public void SpawnNext()
    {
        StartCoroutine(E_Spawn(_levelData.stageDataList[_stage++]));
    }

    private void Awake()
    {
        _levelData = GameManager.instance.levelData;
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
                            Enemy enemy = Instantiate(stageData.spawnDataList[i].prefab,
                                                      PathInformation.instance.startPoints[stageData.spawnDataList[i].startPointIndex].position,
                                                      Quaternion.identity)
                                            .GetComponent<Enemy>();

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
}
