using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.GameElements
{
    public class Merchant : NPC
    {
        public override void StartInteraction(GameObject interactor)
        {
            UIManager.instance.Get<SpendItemShopUI>().Show();
            isInteracting = true;
        }
        public override void FinishInteraction()
        {
            isInteracting = false;
        }

        private void Start()
        {
            StartCoroutine(Init());
        }

        IEnumerator Init()
        {
            SpendItemShopUI spendItemShopUI = null;
            yield return new WaitUntil(() => UIManager.instance.TryGet(out spendItemShopUI));
            spendItemShopUI.onHide += FinishInteraction;
        }
    }
}