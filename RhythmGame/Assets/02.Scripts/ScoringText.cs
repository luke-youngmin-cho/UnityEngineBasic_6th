using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class ScoringText : MonoBehaviour
{
    public static ScoringText instance;
    private TMP_Text _scoreText;
    public int score
    {
        get
        {
            return _score;            
        }
        set
        {
            _after = value;
            _score = value;
            _delta = (int)((_after - _before) / _duration);
        }
    }
    private int _score;

    private int _before;
    private int _after;
    private int _delta;
    private float _duration = 0.1f;
    private StringBuilder _buffer;
    private string format = " {0,3:" + "000" + "}";

    public void Clear()
    {
        _score = _before = _after = _delta = 0;
        _scoreText.text = "0";
    }


    private void Awake()
    {
        instance = this;
        _scoreText = GetComponent<TMP_Text>();
        _buffer = new StringBuilder();
    }

    private void Update()
    {
        if (_before < _after)
        {
            _before += (int)(_delta * Time.deltaTime);

            if (_before > _after)
                _before = _after;

            _buffer.Clear();

            int before1 = _before % 1000;
            int before2 = (_before / 1000) % 1000;
            int before3 = (_before / 1000000) % 1000000;

            if (before3 > 0)
            {
                _buffer.Append(before3);
                _buffer.Append(',');
                _buffer.AppendFormat(" {0,3:" + "000" + "}", before2);
                _buffer.Append(',');
                _buffer.AppendFormat(" {0,3:" + "000" + "}", before1);
            }
            else if (before2 > 0)
            {
                _buffer.Append(before2);
                _buffer.Append(',');
                _buffer.AppendFormat(" {0,3:" + "000" + "}", before1);
            }            
            else
            {
                _buffer.Append(before1);
            }
            _scoreText.text = _buffer.ToString();
        }
    }
}
