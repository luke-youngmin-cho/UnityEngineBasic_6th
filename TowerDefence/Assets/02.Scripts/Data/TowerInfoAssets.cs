using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfoAssets : MonoBehaviour
{
    public static TowerInfoAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<TowerInfoAssets>("TowerInfoAssets"));
            return _instance;
        }
    }
    private static TowerInfoAssets _instance;

    [SerializeField] private List<TowerInfo> _towerInfoList;

    public bool TryGetTowerInfo(TowerType type, int upgradeLevel, out TowerInfo info)
    {
        info = _towerInfoList.Find(x => x.type == type && x.upgradeLevel == upgradeLevel);
        return info;
    }

    public IEnumerable<TowerInfo> GetTowerInfos(Predicate<TowerInfo> match)
    {
        return _towerInfoList.FindAll(match);
    }
}