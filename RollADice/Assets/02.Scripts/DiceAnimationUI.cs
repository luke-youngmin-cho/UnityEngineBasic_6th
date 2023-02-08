using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceAnimationUI : MonoBehaviour
{
    #region Singleton
    public static DiceAnimationUI instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;
    public bool isBusy;

    public void Play(int diceValue, Action onAnimationFinished)
    {
        if (isBusy)
            return;

        isBusy = true;
        StartCoroutine(E_Play(diceValue, onAnimationFinished));
    }

    // 코루틴 (Coroutine) 
    // 함수가 반환시에 다른 함수를 호출하는 형태로 함수끼리 서로 상호작용을 하면서 호출되는 루틴
    //
    // UnityEngine 의 코루틴
    // IEnumerator 를 MonoBehaviour.StartCoroutine() 에 인자로 전달해주면 해당 Enumerator 를 등록하고
    // Update 함수와 협동적으로 Update 이벤트가 끝날때 등록되어있는 IEnumerator의 MoveNext() 함수를 호출해서 
    // 그다음 yield 로직을 수행 할 수 있도록 돌아가는 루틴 
    //
    // yield 키워드
    // C# 에서 Collection 이 있을때 이 Collection 을 '한번 호출에 하나씩' Iterating 하기위한 키워드
    // IEnumerator / IEnumerable 을 반환할때 약식으로 사용하기위한 용도

    private IEnumerator E_Play(int diceValue, Action onAnimationFinished)
    {
        float timeMark = Time.time;
        float timer = 0.0f;
        while (Time.time - timeMark < _duration)
        {
            if (timer < 0f)
            {
                _image.sprite = _sprites[Random.Range(0, _sprites.Length)];
                timer = 1.0f / _speed;
            }
            else
            {
                timer -= Time.deltaTime; // 이런식으로 Time.deltaTime 같은걸 써서 타이머체크하는방법은 좋지않음.
            }

            yield return null;
        }
        _image.sprite = _sprites[diceValue - 1];
        onAnimationFinished?.Invoke();

        yield return new WaitForSeconds(0.5f);
        isBusy = false;
    }
}
