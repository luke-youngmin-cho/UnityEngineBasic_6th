using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _life;
    [SerializeField] private TMP_Text _money;

    private void Start()
    {
        Player.instance.onLifeChanged += (value) => _life.text = value.ToString();
        Player.instance.onMoneyChanged += (value) => _money.text = value.ToString();
        _life.text = Player.instance.life.ToString();
        _money.text = Player.instance.money.ToString();
    }
}