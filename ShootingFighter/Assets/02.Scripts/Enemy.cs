using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value <= 0)
            {
                Score.instance.score += _score;
                if (Random.Range(0.0f, 100.0f) < _itemDropPercent)
                {
                    Instantiate(_bombItemPrefab, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }

            _hp = value;
            _hpSlider.value = (float)value / _hpMax;
        }
    }
    private int _hp;
    [SerializeField] private int _hpMax = 100;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private int _damage = 10;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private int _score;
    [SerializeField] private GameObject _bombItemPrefab;
    [Range(0.0f, 100.0f)]
    [SerializeField] private float _itemDropPercent = 20.0f;


    private void Awake()
    {
        hp = _hpMax;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1<<other.gameObject.layer) & _playerMask) > 0)
        {
            other.GetComponent<Fighter>().hp -= _damage;
            Destroy(gameObject);
        }
    }
}
