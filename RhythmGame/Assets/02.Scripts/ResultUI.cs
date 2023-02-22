using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResultUI : MonoBehaviour
{
    public static ResultUI instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<ResultUI>("ResultUI"));
            return _instance;
        }
    }
    private static ResultUI _instance;

    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _coolCount;
    [SerializeField] private TMP_Text _greatCount;
    [SerializeField] private TMP_Text _goodCount;
    [SerializeField] private TMP_Text _missCount;
    [SerializeField] private TMP_Text _badCount;
    [SerializeField] private TMP_Text _rank;



}