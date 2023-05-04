using UnityEngine;
using TMPro;
using RPG.DataModels;

namespace RPG.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class GoldText : MonoBehaviour
    {
        private void Awake()
        {
            TMP_Text gold = GetComponent<TMP_Text>();
            GoldDataModel goldDataModel = DataModelManager.instance.Get<GoldDataModel>();
            goldDataModel.dataChanged += (value) => gold.text = value.ToString();
            gold.text = goldDataModel.data.ToString();
        }
    }
}
