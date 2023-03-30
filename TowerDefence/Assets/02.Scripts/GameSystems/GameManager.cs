using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState
    {
        Idle,
        LoadLevelData,
        WaitUntilLevelDataLoaded,
        StartLevel,
        InitLevel,
        WaitUntilLevelFinished,
        SuccessLevel,
        FailLevel,
        WaitForUser
    }
    public GameState state;
    public int levelSelected = -1;
    public LevelData levelData;

    public void StartGame()
    {
        if (levelSelected < 0)
            return;

        if (state != GameState.Idle)
            return;

        state = GameState.LoadLevelData;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.Idle:
                break;
            case GameState.LoadLevelData:
                {
                    if (LevelDataAssets.instance.TryGetLevelData(levelSelected, out levelData))
                    {
                        state = GameState.WaitUntilLevelDataLoaded;
                    }
                }
                break;
            case GameState.WaitUntilLevelDataLoaded:
                {
                    if (levelData != null)
                    {
                        state = GameState.StartLevel;
                    }
                }
                break;
            case GameState.StartLevel:
                {
                    SceneManager.LoadScene($"Level{levelSelected}");
                    state = GameState.InitLevel;
                }
                break;
            case GameState.InitLevel:
                {
                    Pathfinder.SetUp();
                    state = GameState.WaitUntilLevelFinished;
                }
                break;
            case GameState.WaitUntilLevelFinished:
                break;
            case GameState.SuccessLevel:
                break;
            case GameState.FailLevel:
                break;
            case GameState.WaitForUser:
                break;
            default:
                break;
        }
    }
}
