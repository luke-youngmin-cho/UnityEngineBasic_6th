using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Datum;
using UnityEngine.EventSystems;
using RPG.DataStructures;
using RPG.DependencySources;
using System;

namespace RPG.UI
{
    public class SpendItemShopPurchaseSlot : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector] public int index;
        public int itemID
        {
            get => _itemID;
            set
            {
                if (value > 0)
                {
                    ItemInfo tmpItemInfo = ItemInfoAssets.instance[value];
                    _itemIcon.sprite = tmpItemInfo.icon;
                    _itemName.text = tmpItemInfo.name;
                    _itemPrice.text = tmpItemInfo.purchasePrice.ToString();
                }
                else
                {
                    _itemIcon.sprite = null;
                    _itemName.text = string.Empty;
                    _itemPrice.text = string.Empty;
                }
                _itemID = value;
            }
        }
        private int _itemID;

        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemName;
        [SerializeField] private TMP_Text _itemPrice;

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
                                            Gold sum = ItemInfoAssets.instance[itemID].purchasePrice * confirmWindow.GetInput();
                                            if (sum > presenter.goldSource.data)
                                            {
                                                UIManager.instance.Get<WarningWindowUI>().Show(string.Empty, "Not enough gold", 1.0f);
                                                return;
                                            }

                                            if (presenter.purchaseCommand.TryExecute(new ItemPair(itemID, confirmWindow.GetInput())))
                                            {
                                                // 아이템 구매 알림
                                            }
                                            else
                                            {
                                                UIManager.instance.Get<WarningWindowUI>().Show(string.Empty, "Not enough inventory space", 1.0f);
                                                return;
                                            }
                                            confirmWindow.Hide();
                                         },
                                         onCancel: null,
                                         content: "Enter purchasing number");
                }

                _clickTimeMark = Time.time;
            }
        }
    }
}