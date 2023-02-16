using UnityEngine;
using UnityEngine.Video;

public static class SongDataLoader
{
    public static bool isLoaded
    {
        get
        {
            return dataLoaded != null && clipLoaded != null;
        }
    }
    public static SongData dataLoaded;
    public static VideoClip clipLoaded;

    public static void Load(string songName)
    {
        dataLoaded = JsonUtility.FromJson<SongData>(Resources.Load<TextAsset>($"SongDatas/{songName}").ToString());
        clipLoaded = Resources.Load<VideoClip>($"VideoClips/{songName}");
    }
}
