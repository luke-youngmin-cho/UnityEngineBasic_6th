using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathInformation : MonoBehaviour
{
    public static PathInformation instance;

    public List<Transform> startPoints;
    public List<Transform> endPoints;

    [Serializable]
    public class Path
    {
        public Transform start => pointList[0];
        public Transform end => pointList[pointList.Count - 1];
        public Transform this[int index] => pointList[index];
        public List<Transform> pointList;
    }
    public List<Path> customizedPathList;


    private void Awake()
    {
        instance = this;
    }
}
