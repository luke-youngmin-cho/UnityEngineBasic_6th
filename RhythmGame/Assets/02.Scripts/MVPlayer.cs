using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class MVPlayer : MonoBehaviour
{
    public static MVPlayer instance;
    private VideoPlayer _videoPlayer;


    public void Play(VideoClip clip)
    {
        _videoPlayer.clip = clip;
        Invoke("Play", NoteSpawnManager.instance.noteFallingTime);
    }

    public void Stop()
    {
        _videoPlayer.Stop();
        _videoPlayer.clip = null;
    }


    private void Awake()
    {
        instance = this;
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Play()
    {
        _videoPlayer.Play();
    }
}
