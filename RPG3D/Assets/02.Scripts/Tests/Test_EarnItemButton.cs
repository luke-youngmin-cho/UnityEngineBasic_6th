using RPG.DataModels;
using RPG.Datum;
using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_EarnItemButton : MonoBehaviour
{
    [SerializeField] private ItemID _itemID;
    [SerializeField] private int _num;

    private void Awake()
    {
        GetComponent<Button>()
             .onClick
             .AddListener(() =>
             {
                 //DataModelManager.instance.Get<InventoryDataModel>().Add(new ItemPair(_itemID.value, _num));
                 UIManager.instance.Get<InventoryUI>()
                    ._presenter
                    .addCommand
                    .TryExecute(new ItemPair(_itemID.value, _num), out int remains);
             });
    }
}
