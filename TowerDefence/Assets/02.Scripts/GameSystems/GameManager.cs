using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState
    {
        Idle,
        LoadLevelData,
        WaitUntilLevelDataLoaded,
        StartLevel,
        WaitUntilLevelFinished,
        SuccessLevel,
        FailLevel,
        WaitForUser
    }
    public GameState state;
    public int levelSelected;
    public LevelData levelData;

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
                break;
            case GameState.StartLevel:
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
