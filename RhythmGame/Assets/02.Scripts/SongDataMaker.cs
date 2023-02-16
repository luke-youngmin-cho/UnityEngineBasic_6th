
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
    /// ��ȭ ����
    /// �뷡������ ���� ����� MV ���
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
    /// ��ȭ ����
    /// MV �����ϰ� �뷡������ ����
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
    /// �����г� ���� �뷡������ ����
    /// </summary>
    public void SaveRecord()
    {
        string dir = UnityEditor.EditorUtility.SaveFilePanelInProject("�뷡 ������ ����", _songData.name, "json", "");
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
    /// ����ϴ� ���Ű�����ؼ� Ű�Է� üũ�ϰ� ��Ʈ������ �����ؼ� �뷡�����Ϳ� �߰���
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
    /// ���� MV ����ð��� ���� Ű �� ��Ʈ ������ ����
    /// </summary>
    private NoteData CreateNoteData(KeyCode key)
    {
        NoteData noteData = new NoteData()
        {
            key = key,
            time = (float)System.Math.Round(_vp.time, 2)
        };
        Debug.Log($"[SongDataMaker] : NoteData ������, {noteData.key} {noteData.time}");   
        return noteData;
    }
}
