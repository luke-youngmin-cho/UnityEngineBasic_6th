using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new TowerInfo", menuName = "TowerDefence/Create TowerInfo")]
public class TowerInfo : ScriptableObject
{
    public TowerType type;
    public int upgradeLevel;
    public int buildPrice;
    public int sellPrice;
    public Sprite icon;
    public Tower prefab;
}
