using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject _selectButtonPrefab;
    [SerializeField] private Transform _content;

    private void Awake()
    {
        GameObject tmp;
        foreach (TowerInfo towerInfo in TowerInfoAssets.instance.GetTowerInfos(x => x.upgradeLevel == 1).OrderBy(x => x.buildPrice))
        {
            tmp = Instantiate(_selectButtonPrefab, _content);
            tmp.GetComponent<Image>().sprite = towerInfo.icon;
            TowerInfo tmpInfo = towerInfo;
            tmp.GetComponent<Button>().onClick.AddListener(() => TowerHandler.instance.ShowPreview(tmpInfo));
            tmp.transform.GetChild(0).GetComponent<TMP_Text>().text = towerInfo.buildPrice.ToString();
        }
    }
}
