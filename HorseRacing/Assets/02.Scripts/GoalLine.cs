using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 도착한 말을 정지시키고 RaceManager 에게 도착했다고 등록해줌
/// </summary>
public class GoalLine : MonoBehaviour
{
    [SerializeField] private RaceManager _raceManager;

    private void OnTriggerEnter(Collider other)
    {
        // GetComponent 는 해당 GameObject 의 모든 컴포넌트들을 검색하는 무거운 연산. 호출을 최소화해야함.
        Horse horse = other.GetComponent<Horse>();
        horse.doMove = false;
        _raceManager.RegisterFinishedHorse(horse);
    }
}
