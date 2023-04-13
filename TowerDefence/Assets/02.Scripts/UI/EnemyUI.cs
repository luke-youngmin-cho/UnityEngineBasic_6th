using Newtonsoft.Json.Linq;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Enemy enemy { get; set; }
    [SerializeField] private Slider _hp;
    [SerializeField] private Vector3 _offset = Vector3.up;
    [SerializeField] private float _blinkTime = 3.0f;
    private float _blinkTimeMark;
    private bool _isCorouting;

    public static EnemyUI Create(Enemy enemy)
    {
        EnemyUI enemyUI = Instantiate(Resources.Load<EnemyUI>("EnemyUI"));
        enemyUI.enemy = enemy;
        return enemyUI;
    }

    private void Start()
    {
        enemy.onHpChanged += Show;
    }

    public void Show()
    {
        _hp.minValue = enemy.hpMin;
        _hp.maxValue = enemy.hpMax;
        _hp.value = enemy.hp;
        gameObject.SetActive(true);
        if (_isCorouting)
        {
            _blinkTimeMark = Time.time;
        }
        else
        {
            _isCorouting = true;
            StartCoroutine(E_Disable());
        }
    }

    private void Show(float value)
    {
        _hp.value = value;
        gameObject.SetActive(true);
        if (_isCorouting)
        {
            _blinkTimeMark = Time.time;
        }
        else
        {
            _isCorouting = true;
            StartCoroutine(E_Disable());
        }
    }

    public void Hide()
    {
        if (_isCorouting)
            StopAllCoroutines();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = enemy.transform.position + _offset;
    }

    private IEnumerator E_Disable()
    {
        _blinkTimeMark = Time.time;
        while (Time.time - _blinkTimeMark < _blinkTime)
        {
            yield return null;
        }
        Hide();
        _isCorouting = false;
    }
}
