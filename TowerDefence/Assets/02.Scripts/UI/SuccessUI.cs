using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SuccessUI : MonoBehaviour
{
    [SerializeField] private Button _lobby;
    [SerializeField] private Button _retry;
    [SerializeField] private Button _next;

    private void Awake()
    {
        _lobby.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Lobby");
        });

        _retry.onClick.AddListener(() =>
        {
            GameManager.instance.RestartGame();
        });

        if (LevelDataAssets.instance.TryGetLevelData(GameManager.instance.levelSelected, out LevelData data))
        {
            _next.onClick.AddListener(() =>
            {
                GameManager.instance.levelSelected++;
                GameManager.instance.StartGame();
            });
        }
        else
        {
            _next.gameObject.SetActive(false);
        }
    }
}
