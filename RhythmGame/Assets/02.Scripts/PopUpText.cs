using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class PopUpText : MonoBehaviour
{
    private TMP_Text _text;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _dir = Vector3.up;
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private float _fadeSpeed = 0.5f;
    private Color _colorOrigin;

    public void PopUp()
    {
        ResetPos();
        _text.color = _colorOrigin;
        gameObject.SetActive(true);
    }

    public void PopUp(string text)
    {
        _text.text = text;
        PopUp();
    }

    public void ResetPos()
    {
        transform.position = _startPos;
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _colorOrigin = _text.color;
    }

    private void Update()
    {
        transform.Translate(_dir * _moveSpeed * Time.deltaTime);

        float a = _text.color.a - _fadeSpeed * Time.deltaTime;

        if (a > 0.0f)
        {
            _text.color = new Color(_colorOrigin.r, _colorOrigin.g, _colorOrigin.b, a);
        }
        else
        {
            gameObject.SetActive(false);
            _text.color = _colorOrigin;
        }
    }
}
