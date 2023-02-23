using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Button _replay;
    [SerializeField] private Button _lobby;


    public void Show()
    {
        _score.text = GameStatus.score.ToString();
        _coolCount.text = GameStatus.coolCount.ToString();
        _greatCount.text = GameStatus.greatCount.ToString();
        _goodCount.text = GameStatus.goodCount.ToString();
        _missCount.text = GameStatus.missCount.ToString();
        _badCount.text = GameStatus.badCount.ToString();

        float _scoreRatio = (float)GameStatus.score / (PlaySettings.SCORE_COOL * (GameStatus.coolCount + GameStatus.greatCount + GameStatus.goodCount + GameStatus.missCount + GameStatus.badCount));
        if (_scoreRatio >= 0.99f) _rank.text = "SSS";
        else if (_scoreRatio > 0.97) _rank.text = "SS";
        else if (_scoreRatio > 0.95) _rank.text = "S";
        else if (_scoreRatio > 0.9) _rank.text = "A";
        else if (_scoreRatio > 0.85) _rank.text = "B";
        else if (_scoreRatio > 0.80) _rank.text = "C";
        else if (_scoreRatio > 0.75) _rank.text = "D";
        else if (_scoreRatio > 0.70) _rank.text = "E";
        else _rank.text = "F";
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        _replay.onClick.AddListener(() =>
        {
            if (GameManager.instance.state == GameManager.GameStates.WaitForUser)
            {
                GameManager.instance.state = GameManager.GameStates.StartGame;
                gameObject.SetActive(false);
            }
        });

        _lobby.onClick.AddListener(() =>
        {
            if (GameManager.instance.state == GameManager.GameStates.WaitForUser)
            {
                GameManager.instance.state = GameManager.GameStates.Idle;
                SceneManager.LoadScene("SongSelect");
            }
        });
    }
}