using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public enum GameStates
    {
        Idle,
        LoadSongData,
        WaitUntilSongDataLoaded,
        StartGame,
        WaitUntilGameFinished,
        DisplayScore,
        WaitForUser
    }
    public GameStates state;

    public string songSelected = string.Empty;

    private void Update()
    {
        switch (state)
        {
            case GameStates.Idle:
                break;
            case GameStates.LoadSongData:
                {
                    SceneManager.LoadScene("SongPlay");
                    SongDataLoader.Load(songSelected);
                    state = GameStates.WaitUntilSongDataLoaded;
                }
                break;
            case GameStates.WaitUntilSongDataLoaded:
                {
                    if (SongDataLoader.isLoaded)
                    {                        
                        state = GameStates.StartGame;
                    }
                }
                break;
            case GameStates.StartGame:
                {
                    if (MVPlayer.instance != null)
                    {
                        MVPlayer.instance.Play(SongDataLoader.clipLoaded);
                        NoteSpawnManager.instance.StartSpawn(SongDataLoader.dataLoaded.notes);
                        state = GameStates.WaitUntilGameFinished;
                    }
                }
                break;
            case GameStates.WaitUntilGameFinished:
                break;
            case GameStates.DisplayScore:
                break;
            case GameStates.WaitForUser:
                break;
            default:
                break;
        }
    }
}