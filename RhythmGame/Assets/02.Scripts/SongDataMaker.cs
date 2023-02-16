
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class SongDataMaker : MonoBehaviour
{
    private KeyCode[] _keys = { KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.L };
    [SerializeField] private SongData _songData;
    private VideoPlayer _vp;
    private bool _isRecording;

    /// <summary>
    /// 녹화 시작
    /// 노래데이터 새로 만들고 MV 재생
    /// </summary>
    public void StartRecord()
    {
        if (_isRecording)
            return;

        _isRecording = true;
        _songData = new SongData(_vp.clip.name);
        _vp.Play();
    }

    /// <summary>
    /// 녹화 종료
    /// MV 종료하고 노래데이터 저장
    /// </summary>
    public void StopRecord()
    {
        if (_isRecording == false)
            return;

        _vp.Stop();
        SaveRecord();
        _songData = null;        
    }

    /// <summary>
    /// 저장패널 띄우고 노래데이터 저장
    /// </summary>
    public void SaveRecord()
    {
        string dir = UnityEditor.EditorUtility.SaveFilePanelInProject("노래 데이터 저장", _songData.name, "json", "");
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(_songData));
    }

    private void Awake()
    {
        _vp = GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (_isRecording)
            Recording();
    }

    /// <summary>
    /// 사용하는 모든키에대해서 키입력 체크하고 노트데이터 생성해서 노래데이터에 추가함
    /// </summary>
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

    /// <summary>
    /// 현재 MV 재생시간에 대한 키 값 노트 데이터 생성
    /// </summary>
    private NoteData CreateNoteData(KeyCode key)
    {
        NoteData noteData = new NoteData()
        {
            key = key,
            time = (float)System.Math.Round(_vp.time, 2)
        };
        Debug.Log($"[SongDataMaker] : NoteData 생성됨, {noteData.key} {noteData.time}");   
        return noteData;
    }
}
