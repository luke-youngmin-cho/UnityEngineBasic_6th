using RPG.DataModels;
using RPG.DataStructures;
using RPG.Datum;
using RPG.DependencySources;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.UI
{
    public class SpendItemShopSellSlot : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector] public int index;
        public ItemPair itemPair
        {
            get
            {
                return _itemPair;
            }
            set
            {
                if (value.id > 0 && value.num > 0)
                {
                    _icon.sprite = ItemInfoAssets.instance[value.id].icon;
                    _icon.color = Color.white;
                    _num.text = value.num.ToString();
                }
                else
                {
                    _icon.sprite = null;
                    _icon.color = Color.clear;
                    _num.text = string.Empty;
                }
                _itemPair = value;
            }
        }
        private ItemPair _itemPair;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _num;

        private float _clickTimeMark;
        private const float DOUBLE_CLICK_TERM = 0.5f;
        private SpendItemShopUI spendItemShop;

        private void Awake()
        {
            spendItemShop = UIManager.instance.Get<SpendItemShopUI>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == 0)
            {
                if (Time.time - _clickTimeMark < DOUBLE_CLICK_TERM)
                {
                    ConfirmWindowWithInputFieldUI confirmWindow = UIManager.instance.Get<ConfirmWindowWithInputFieldUI>();
                    confirmWindow.Show(onConfirm: () =>
                                                  {
                                                      SpendItemShopPresenter presenter = spendItemShop.GetPresenter();
                                                      if (presenter.inventorySource[index].num < confirmWindow.GetInput())
                                                      {
                                                          UIManager.instance.Get<WarningWindowUI>().Show(string.Empty, "Not enough items", 1.0f);
                                                          return;
                                                      }

                                                      if (presenter.sellCommand.TryExecute(new ItemPair(itemPair.id, confirmWindow.GetInput())))
                                                      {
                                                          // 아이템 판매알림
                                                      }
                                                      else
                                                      {
                                                          throw new System.Exception($"[SpendItemShopSellSlot] : Failed to sell {confirmWindow.GetInput()} of  {itemPair.id}");
                                                      }
                                                      confirmWindow.Hide();
                                                  },
                                         onCancel: null,
                                         content: "Enter selling number");
                }

                _clickTimeMark = Time.time;
            }
        }
    }
}