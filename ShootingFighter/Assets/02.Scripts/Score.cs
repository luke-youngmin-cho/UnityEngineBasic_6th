using UnityEngine;
using TMPro;

/// <summary>
/// 현재 점수 및 점수 UI
/// </summary>
public class Score : MonoBehaviour
{
    // 싱글톤 패턴 : 
    // static 멤버변수로 인스턴스를 참조하는 형태
    // 주로 런타임중에 단 하나만 존재하는 객체가 있고 다른 클래스들이 참조하는 경우가 잦은 경우 사용함.
    public static Score instance;

    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            _scoreText.text = value.ToString();
        }
    }
    private int _score;
    [SerializeField] private TMP_Text _scoreText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
