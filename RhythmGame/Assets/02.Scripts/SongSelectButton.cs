using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SongSelectButton : MonoBehaviour
{
    [SerializeField] private string songName;
    private Button _button;


    public void Select()
    {        
        GameManager.instance.songSelected = songName;
    }


    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Select);
    }
}
