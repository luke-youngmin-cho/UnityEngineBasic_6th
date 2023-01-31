using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : MonoBehaviour
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
                Destroy(gameObject);

            _hp = value;
            _hpSlider.value = (float)value / _hpMax;
        }
    }
    private int _hp;
    [SerializeField] private int _hpMax = 100;
    [SerializeField] private Slider _hpSlider;

    private void Awake()
    {
        hp = _hpMax;
    }
}
