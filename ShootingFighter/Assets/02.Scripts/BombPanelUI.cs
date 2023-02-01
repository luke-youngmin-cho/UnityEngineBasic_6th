using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _bombImages;

    public void Refresh(int num)
    {
        for (int i = 0; i < _bombImages.Length; i++)
        {
            if (i < num)
                _bombImages[i].SetActive(true);
            else
                _bombImages[i].SetActive(false);
        }
    }

    private void Start()
    {
        Refresh(FighterFire.instance.bombNum);
        FighterFire.instance.OnBombNumChanged += Refresh;
    }
}
