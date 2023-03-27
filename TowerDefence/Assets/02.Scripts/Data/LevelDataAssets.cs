using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataAssets : MonoBehaviour
{
    public static LevelDataAssets instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<LevelDataAssets>("LevelDataAssets"));
            }
            return _instance;
        }
    }
    private static LevelDataAssets _instance;

    [SerializeField] private List<LevelData> _dataList;

    public bool TryGetLevelData(int level, out LevelData data)
    {
        data = _dataList.Find(x => x.level == level);
        return data != null;
    }
}
