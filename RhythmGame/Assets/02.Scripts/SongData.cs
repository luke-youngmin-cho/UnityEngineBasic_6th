using System;
using System.Collections.Generic;

/// <summary>
/// � �뷡�� � ��Ʈ��� �̷�����ִ����� ���� ������
/// </summary>
[Serializable]
public class SongData
{
    public string name;
    public List<NoteData> notes;

    public SongData(string name)
    {
        this.name = name;
        notes = new List<NoteData>();
    }
}
