using System;
using UnityEngine;

[Serializable]
public class SpawnData
{
    public GameObject prefab;
    public int num;
    public float term;
    public float startDelay;
    public int startPointIndex;
    public int endPointIndex;
}
