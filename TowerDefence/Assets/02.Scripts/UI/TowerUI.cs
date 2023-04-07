using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUI : MonoBehaviour
{
    public static TowerUI instance;
    [SerializeField] private Button _upgrade;
    [SerializeField] private TMP_Text _upgradePrice;
    [SerializeField] private Button _sell;
    [SerializeField] private TMP_Text _sellPrice;
    private TowerInfo _upgradeTowerInfo;
    [SerializeField] Vector3 _offset;

    public void Show(Tower tower)
    {
        // 다음레벨의 타워정보 받아오기
        if (TowerInfoAssets.instance.TryGetTowerInfo(tower.type, tower.upgradeLevel + 1, out _upgradeTowerInfo))
        {
            Player.instance.onMoneyChanged += RefreshUpgradePriceColor;
            RefreshUpgradePriceColor(Player.instance.money);
            _upgradePrice.text = _upgradeTowerInfo.buildPrice.ToString();
            _upgrade.onClick.RemoveAllListeners();
            _upgrade.onClick.AddListener(() =>
            {
                Player.instance.money -= _upgradeTowerInfo.buildPrice;
                Node node = tower.node;
                node.DestroyTowerHere();
                node.TryBuildTowerHere(_upgradeTowerInfo, out Tower built);
                Hide();
                Show(built);
            });
            _upgrade.gameObject.SetActive(true);
        }
        else
        {
            _upgrade.gameObject.SetActive(false);
        }

        // 현재 타워 정보 받아오기
        if (TowerInfoAssets.instance.TryGetTowerInfo(tower.type, tower.upgradeLevel, out TowerInfo towerInfo))
        {
            _sellPrice.text = towerInfo.sellPrice.ToString();
            _sell.onClick.RemoveAllListeners();
            _sell.onClick.AddListener(() =>
            {
                Destroy(tower.gameObject);
                Player.instance.money += towerInfo.sellPrice;
                Hide();
            });
        }
        transform.position = tower.transform.position + _offset;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Player.instance.onMoneyChanged -= RefreshUpgradePriceColor;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }

    private void RefreshUpgradePriceColor(int money)
    {
        _upgradePrice.color = money < _upgradeTowerInfo.buildPrice ? Color.red : Color.black;
    }
}
