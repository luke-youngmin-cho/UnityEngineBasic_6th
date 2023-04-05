using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new LevelData", menuName = "TowerDefence/Create new LevelData")]
public class LevelData : ScriptableObject
{
    public int level;
    public int life;
    public int money;
    public List<StageData> stageDataList;
}
