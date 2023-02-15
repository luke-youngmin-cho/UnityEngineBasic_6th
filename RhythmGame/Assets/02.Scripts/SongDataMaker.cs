
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class SongDataMaker : MonoBehaviour
{
    private KeyCode[] _keys = { KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.L };
    private SongData _songData;
    private VideoPlayer _vp;
    private bool _isRecording;

    public void StartRecord()
    {
        if (_isRecording)
            return;

        _isRecording = true;
        _songData = new SongData();
        _vp.Play();
    }

    public void SaveRecord()
    {

    }

    private void Update()
    {
        if (_isRecording)
            Recording();
    }

    private void Recording()
    {
        foreach (KeyCode key in _keys)
        {
            if (Input.GetKeyDown(key))
            {
                _songData.notes.Add(CreateNoteData(key));
            }
        }
    }

    private NoteData CreateNoteData(KeyCode key)
    {
        NoteData noteData = new NoteData()
        {
            key = key,
            time = (float)System.Math.Round(_vp.time, 2)
        };
        Debug.Log($"[SongDataMaker] : NoteData »ý¼ºµÊ, {noteData.key} {noteData.time}");   
        return noteData;
    }
}
