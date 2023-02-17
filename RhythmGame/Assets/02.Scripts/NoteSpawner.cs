using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public KeyCode key;
    [SerializeField] private Note _notePrefab;
    private Color _color;

    public void Spawn()
    {
        Note note = Instantiate(_notePrefab, transform.position, Quaternion.identity);
        note.GetComponent<SpriteRenderer>().color = _color;
        note.transform.localScale = transform.lossyScale;
        note.speed = PlaySettings.speed;
    }

    private void Awake()
    {
        _color = GetComponent<SpriteRenderer>().color;
    }
}